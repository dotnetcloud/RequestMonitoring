using DotNetCloud.RequestMonitoring.Core.Abstractions;
using Microsoft.AspNetCore.Http;

namespace DotNetCloud.RequestMonitoring.Core.TagProducers
{
    internal class UserAgentTagProducer : TagProducerBase, ITagProducer
    {
        private readonly ITagPrefixProvider _prefixProvider;

        public UserAgentTagProducer(ITagPrefixProvider prefixProvider) => _prefixProvider = prefixProvider;

        protected override string Prefix => _prefixProvider.UserAgent;

        public override string GetValue(HttpContext context) =>
            context.Items.TryGetValue(RequestMonitoringMiddleware.UserAgentItem, out var userAgentItem) && userAgentItem is string userAgent
                ? userAgent
                : string.Empty;
    }
}