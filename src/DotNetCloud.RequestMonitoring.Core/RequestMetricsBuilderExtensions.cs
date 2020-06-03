using DotNetCloud.RequestMonitoring.Core.Abstractions;
using DotNetCloud.RequestMonitoring.Core.DependencyInjection;
using DotNetCloud.RequestMonitoring.Core.TagProducers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace DotNetCloud.RequestMonitoring.Core
{
    public static class RequestMetricsBuilderExtensions
    {
        public static IRequestMetricsBuilder TagWithUserAgent(this IRequestMetricsBuilder builder)
        {
            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ITagProducer, UserAgentTagProducer>());
            return builder;
        }

        public static IRequestMetricsBuilder TagWithRouteAndMethod(this IRequestMetricsBuilder builder)
        {
            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ITagProducer, RouteTagProducer>());
            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ITagProducer, MethodTagProducer>());
            return builder;
        }
    }
}
