namespace DotNetCloud.RequestMonitoring.DataDog
{
    /// <summary>
    /// Defines common metric names which can be used when sending metrics to DataDog.
    /// </summary>
    public static class DataDogMetricNames
    {
        /// <summary>
        /// An HTTP request has been received and a response sent.
        /// </summary>
        public const string HttpRequestMetric = "http_request";

        /// <summary>
        /// The time taken to handle an HTTP request.
        /// </summary>
        public const string HttpRequestMilliseconds = "http_request_ms";
    }
}