namespace DotNetCloud.RequestMonitoring.Core.Abstractions
{
    public interface ITagPrefixProvider
    {
        string RequestRoute { get; }

        string RequestMethod { get; }

        string UserAgent { get; }

        string StatusCode { get; }

        string StatusCodeRange { get; }

        string RequestResult { get; }
    }
}