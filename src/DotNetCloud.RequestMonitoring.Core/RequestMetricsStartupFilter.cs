using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace DotNetCloud.RequestMonitoring.Core
{
    internal class RequestMetricsStartupFilter : IStartupFilter
    {
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            return builder =>
            {
                builder.UseMiddleware<RequestMonitoringMiddleware>();
                next(builder);
            };
        }
    }
}