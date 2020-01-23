using MotiNet.AspNetCore.Mvc;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class MvcServiceCollectionExtensions
    {
        public static IServiceCollection AddMotiNet(this IServiceCollection services)
        {
            services.AddScoped<MvcHelper>();

            return services;
        }
    }
}
