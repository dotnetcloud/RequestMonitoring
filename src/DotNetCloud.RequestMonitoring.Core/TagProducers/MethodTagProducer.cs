using DotNetCloud.RequestMonitoring.Core.Abstractions;
using Microsoft.AspNetCore.Http;

namespace DotNetCloud.RequestMonitoring.Core.TagProducers
{
    internal class MethodTagProducer : TagProducerBase, ITagProducer
    {
        private readonly ITagPrefixProvider _prefixProvider;

        public MethodTagProducer(ITagPrefixProvider prefixProvider) => _prefixProvider = prefixProvider;

        protected override string Prefix => _prefixProvider.RequestMethod;

        public override string GetValue(HttpContext context) => context.Request.Method;
    }
}