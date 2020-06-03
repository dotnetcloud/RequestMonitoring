using System;
using DotNetCloud.RequestMonitoring.Core;
using DotNetCloud.RequestMonitoring.Core.Abstractions;
using DotNetCloud.RequestMonitoring.Core.DependencyInjection;
using DotNetCloud.RequestMonitoring.Core.TagProducers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class RequestMetricsServiceCollectionExtensions
    {
        public static IRequestMetricsBuilder AddDefaultRequestMetrics(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddSingleton<IRequestTagBuilder, RequestTagBuilder>();

            services.TryAddEnumerable(ServiceDescriptor.Singleton<ITagProducer, RouteTagProducer>());
            services.TryAddEnumerable(ServiceDescriptor.Singleton<ITagProducer, MethodTagProducer>());
            services.TryAddEnumerable(ServiceDescriptor.Singleton<ITagProducer, UserAgentTagProducer>());
            services.TryAddEnumerable(ServiceDescriptor.Singleton<ITagProducer, StatusCodeTagProducer>());
            services.TryAddEnumerable(ServiceDescriptor.Singleton<ITagProducer, StatusCodeRangeTagProducer>());
            services.TryAddEnumerable(ServiceDescriptor.Singleton<ITagProducer, RequestResultTagProducer>());

            services.AddTransient<IStartupFilter, RequestMetricsStartupFilter>();

            return new DefaultRequestMetricsBuilder(services);
        }
    }
}