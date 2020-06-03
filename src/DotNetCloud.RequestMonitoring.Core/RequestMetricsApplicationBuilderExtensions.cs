using Microsoft.AspNetCore.Builder;

namespace DotNetCloud.RequestMonitoring.Core
{
    public static class RequestMetricsApplicationBuilderExtensions
    {
        /// <summary>
        /// Add request metric recording. This middleware should be registered early to wrap the majority of the application pipeline.
        /// </summary>
        /// <param name="app">The <see cref="IApplicationBuilder"/>.</param>
        /// <returns>The <see cref="IApplicationBuilder"/>.</returns>
        public static IApplicationBuilder UseRequestMetrics(this IApplicationBuilder app) => app.UseMiddleware<RequestMonitoringMiddleware>();
    }
}