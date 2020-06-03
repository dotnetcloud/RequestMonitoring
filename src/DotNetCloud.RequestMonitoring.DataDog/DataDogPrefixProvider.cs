using DotNetCloud.RequestMonitoring.Core.Abstractions;

namespace DotNetCloud.RequestMonitoring.DataDog
{
    /// <summary>
    /// Defines common tag prefixes which can be used when sending metrics to DataDog.
    /// </summary>
    public class DataDogPrefixProvider : ITagPrefixProvider
    {
        /// <summary>
        /// This tag should be used to identify the route or path of the request.
        /// </summary>
        public string RequestRoute => "request_route:";

        /// <summary>
        /// This tag should be used to identify the HTTP method of the request.
        /// </summary>
        public string RequestMethod => "request_method:";

        /// <summary>
        /// This tag should be used to identify the user agent initiating the request.
        /// </summary>
        public string UserAgent => "user_agent:";

        /// <summary>
        /// This tag should be used to identify the status code of a HTTP response.
        /// </summary>
        public string StatusCode => "status_code:";

        /// <summary>
        /// This tag should be used to identify the status code group (2xx, 3xx etc.) to which the status code of a HTTP response belongs.
        /// </summary>
        public string StatusCodeRange => "status_code_range:";

        /// <summary>
        /// <para>This tag should be used to identify the overall result of the request.</para>
        /// <para>Status codes less than 400 are successful, 400 or greater are failures.</para>
        /// </summary>
        public string RequestResult => "request_result:";
    }
}