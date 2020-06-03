using DotNetCloud.RequestMonitoring.Core.Abstractions;
using Microsoft.AspNetCore.Http;

namespace DotNetCloud.RequestMonitoring.Core.TagProducers
{
    internal class RequestResultTagProducer : TagProducerBase, ITagProducer
    {
        private readonly ITagPrefixProvider _prefixProvider;

        public RequestResultTagProducer(ITagPrefixProvider prefixProvider) => _prefixProvider = prefixProvider;

        protected override string Prefix => _prefixProvider.RequestResult;

        public override string GetValue(HttpContext context) => context.Response.StatusCode < 400 ? "success" : "failure";
    }
}