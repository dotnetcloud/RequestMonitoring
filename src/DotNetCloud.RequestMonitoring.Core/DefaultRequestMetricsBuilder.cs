using DotNetCloud.RequestMonitoring.Core.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetCloud.RequestMonitoring.Core
{
    internal class DefaultRequestMetricsBuilder : IRequestMetricsBuilder
    {
        public DefaultRequestMetricsBuilder(IServiceCollection services) => Services = services;

        public IServiceCollection Services { get; }
        
        public IServiceCollection Build() => Services;
    }
}
