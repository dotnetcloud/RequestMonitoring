using DotNetCloud.RequestMonitoring.Core.Abstractions;
using Microsoft.AspNetCore.Http;

namespace DotNetCloud.RequestMonitoring.Core.TagProducers
{
    internal class StatusCodeRangeTagProducer : TagProducerBase, ITagProducer
    {
        private readonly ITagPrefixProvider _prefixProvider;

        public StatusCodeRangeTagProducer(ITagPrefixProvider prefixProvider) => _prefixProvider = prefixProvider;
        
        protected override string Prefix => _prefixProvider.StatusCodeRange;

        public override string GetValue(HttpContext context)
        {
            var initialStatusCode = context.Response.StatusCode;

            // Optimised, unrolled loop to get the first digit
            if (initialStatusCode >= 100000000) initialStatusCode /= 100000000;
            if (initialStatusCode >= 10000) initialStatusCode /= 10000;
            if (initialStatusCode >= 100) initialStatusCode /= 100;
            if (initialStatusCode >= 10) initialStatusCode /= 10;

            return $"{initialStatusCode}xx";
        }
    }
}