namespace DotNetCloud.RequestMonitoring.Core.Abstractions
{
    /// <summary>
    /// Provides methods for recording metrics.
    /// </summary>
    public interface IRequestMetricRecorder
    {
        // Records request metrics to the underlying metric system.
        void RecordHttpResponse(long elapsedMilliseconds, string[] tags);
    }
}