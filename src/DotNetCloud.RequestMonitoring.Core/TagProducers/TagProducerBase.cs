using Microsoft.AspNetCore.Http;

namespace DotNetCloud.RequestMonitoring.Core.TagProducers
{
    public abstract class TagProducerBase
    {
        protected abstract string Prefix { get; }

        public string ProduceTag(HttpContext context)
        {
            string value;

            return string.IsNullOrEmpty(Prefix) || string.IsNullOrEmpty(value = GetValue(context))
                ? null
                : $"{Prefix}{value}";
        }

        public abstract string GetValue(HttpContext context);
    }
}