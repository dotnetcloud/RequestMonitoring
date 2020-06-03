using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DotNetCloud.RequestMonitoring.Core.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;

namespace DotNetCloud.RequestMonitoring.Core
{
    /// <summary>
    /// Middleware which wraps the entire request pipeline and records metrics used to monitor the request performance and responses.
    /// </summary>
    public class RequestMonitoringMiddleware
    {
        public const string Unknown = "Unknown";
        public const string UserAgentItem = "UserAgent";

        private readonly RequestDelegate _next;
        private readonly IRequestMetricRecorder _metricRecorder;
        private readonly IRequestTagBuilder _tagBuilder;
        private readonly ILogger<RequestMonitoringMiddleware> _logger;
        
        public RequestMonitoringMiddleware(RequestDelegate next, IRequestMetricRecorder metricRecorder, IRequestTagBuilder tagBuilder, ILogger<RequestMonitoringMiddleware> logger)
        {
            _next = next;
            _metricRecorder = metricRecorder;
            _tagBuilder = tagBuilder;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var startTicks = Stopwatch.GetTimestamp();

            context.Items[UserAgentItem] = context.Request.Headers.TryGetValue(HeaderNames.UserAgent, out var userAgentValue) && !StringValues.IsNullOrEmpty(userAgentValue) 
                ? userAgentValue.First() 
                : Unknown;

            try
            {
                await _next(context);
            }
            catch (OperationCanceledException) when (context.RequestAborted.IsCancellationRequested)
            {
                _logger.LogInformation("The caller cancelled the HTTP request. Returning 499 'Client Closed Request' status code.");

                context.Response.StatusCode = 499; // 499 Client Closed Request
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An unhandled exception was thrown by the applications.");

                context.Response.StatusCode = 500;
            }

            var stopTicks = Stopwatch.GetTimestamp();

            var totalTicks = stopTicks - startTicks;

            var ns = 1000000000.0 * (double)totalTicks / Stopwatch.Frequency;
            var ms = ns / 1000000.0;
            
            try
            {
                var endpoint = context.GetEndpoint();

                if (endpoint is Endpoint && endpoint.Metadata.GetMetadata<ExcludeFromRequestMetricsAttribute>() is object)
                {
                    return; // skip metrics recording
                }

                var tags = _tagBuilder.BuildTags(context);

                foreach (var tag in tags)
                {
                    _logger.LogTrace("Tagging metrics with {TagValue}", tag);
                }

                _metricRecorder.RecordHttpResponse((long)ms, tags);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred when recording request metrics.");
            }   
        }
    }
}
