using DotNetCloud.RequestMonitoring.Core.Abstractions;
using DotNetCloud.RequestMonitoring.Core.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetCloud.RequestMonitoring.DataDog
{
    public static class DataDogRequestMetricsExtensions
    {
        /// <summary>
        /// Adds recording of metrics to DataDog.
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IRequestMetricsBuilder WithDataDogRecorder(this IRequestMetricsBuilder builder)
        {
            builder.Services.AddSingleton<IRequestMetricRecorder, DataDogRequestMetricRecorder>();
            builder.Services.AddSingleton<ITagPrefixProvider, DataDogPrefixProvider>();

            return builder;
        }
    }
}