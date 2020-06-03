using DotNetCloud.RequestMonitoring.Core.Abstractions;
using StatsdClient;

namespace DotNetCloud.RequestMonitoring.DataDog
{
    /// <summary>
    /// Provides methods for recording metrics to DataDog via <see cref="DogStatsd"/>.
    /// </summary>
    internal class DataDogRequestMetricRecorder : IRequestMetricRecorder
    {
        /// <inheritdoc />
        public void RecordHttpResponse(long elapsedMilliseconds, string[] tags)
        {
            DogStatsd.Increment(DataDogMetricNames.HttpRequestMetric, 1, 1, tags);
            DogStatsd.Distribution(DataDogMetricNames.HttpRequestMilliseconds, elapsedMilliseconds, 1, tags);
        }
    }
}
