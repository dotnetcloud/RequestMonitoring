using Microsoft.AspNetCore.Http;

namespace DotNetCloud.RequestMonitoring.Core.Abstractions
{
    public interface ITagProducer
    {
        string ProduceTag(HttpContext context);

        string GetValue(HttpContext context);
    }
}