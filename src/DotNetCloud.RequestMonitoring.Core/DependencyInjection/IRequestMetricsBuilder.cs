using Microsoft.Extensions.DependencyInjection;

namespace DotNetCloud.RequestMonitoring.Core.DependencyInjection
{
    public interface IRequestMetricsBuilder
    {
        /// <summary>
        /// Gets the application service collection.
        /// </summary>
        IServiceCollection Services { get; }

        public IServiceCollection Build();
    }
}
