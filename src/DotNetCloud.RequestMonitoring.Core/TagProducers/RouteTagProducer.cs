using DotNetCloud.RequestMonitoring.Core.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace DotNetCloud.RequestMonitoring.Core.TagProducers
{
    internal class RouteTagProducer : TagProducerBase, ITagProducer
    {
        private readonly ITagPrefixProvider _prefixProvider;

        public RouteTagProducer(ITagPrefixProvider prefixProvider) => _prefixProvider = prefixProvider;

        protected override string Prefix => _prefixProvider.RequestRoute;

        public override string GetValue(HttpContext context) =>
            context.GetEndpoint() is RouteEndpoint routeEndpoint
                ? routeEndpoint.RoutePattern.RawText
                : context.Request.Path.Value;
    }
}