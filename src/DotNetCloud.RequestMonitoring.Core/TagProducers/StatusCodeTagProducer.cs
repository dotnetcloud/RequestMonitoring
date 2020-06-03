using DotNetCloud.RequestMonitoring.Core.Abstractions;
using Microsoft.AspNetCore.Http;

namespace DotNetCloud.RequestMonitoring.Core.TagProducers
{
    internal class StatusCodeTagProducer : TagProducerBase, ITagProducer
    {
        private readonly ITagPrefixProvider _prefixProvider;

        public StatusCodeTagProducer(ITagPrefixProvider prefixProvider) => _prefixProvider = prefixProvider;


        protected override string Prefix => _prefixProvider.StatusCode;

        public override string GetValue(HttpContext context) => context.Response.StatusCode.ToString();
    }
}