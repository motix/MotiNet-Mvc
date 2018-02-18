using MotiNet.AspNetCore.Compozr;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class MvcServiceCollectionExtensions
    {
        public static IServiceCollection AddCompozr(this IServiceCollection services)
        {
            services.AddScoped<CompozrHelper>();

            return services;
        }
    }
}
