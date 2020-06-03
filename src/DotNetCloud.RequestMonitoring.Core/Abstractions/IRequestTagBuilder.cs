using Microsoft.AspNetCore.Http;

namespace DotNetCloud.RequestMonitoring.Core.Abstractions
{
    public interface IRequestTagBuilder
    {
        string[] BuildTags(HttpContext context);
    }
}