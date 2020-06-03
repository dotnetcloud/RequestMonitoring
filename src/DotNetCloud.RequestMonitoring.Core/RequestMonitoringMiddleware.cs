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
            var sw = Stopwatch.StartNew();

            var userAgent = Unknown;

            if (context.Request.Headers.TryGetValue(HeaderNames.UserAgent, out var userAgentValue) && !StringValues.IsNullOrEmpty(userAgentValue))
                userAgent = userAgentValue.First();

            context.Items[UserAgentItem] = userAgent;

            try
            {
                await _next(context);
            }
            catch (OperationCanceledException) when (context.RequestAborted.IsCancellationRequested)
            {
                _logger.LogInformation("The caller cancelled the HTTP request. Returning 499 status code.");

                context.Response.StatusCode = 499; // 499 Client Closed Request
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An unhandled exception was thrown by the applications");

                context.Response.StatusCode = 500;
            }

            var milliseconds = sw.ElapsedMilliseconds;

            var tags = _tagBuilder.BuildTags(context);

            _metricRecorder.RecordHttpResponse(milliseconds, tags);
        }
    }
}