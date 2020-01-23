using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Template;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.ObjectPool;
using MotiNet.AspNetCore.GlobalRouteValuesLinkGenerator;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class GlobalRouteValuesLinkGeneratorServiceCollectionExtensions
    {
        public static IServiceCollection AddGlobalRouteValuesLinkGenerator(
            this IServiceCollection services,
            Action<GlobalRouteValuesLinkGeneratorOptions> configureOptions)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (configureOptions == null)
            {
                throw new ArgumentNullException(nameof(configureOptions));
            }

            services.TryAddSingleton<ObjectPool<UriBuildingContext>>(s =>
            {
                var provider = s.GetRequiredService<ObjectPoolProvider>();
                return provider.Create<UriBuildingContext>(new UriBuilderContextPooledObjectPolicy());
            });
            services.TryAddSingleton<GlobalRouteValuesLinkGeneratorTemplateBinderFactory, DefaultTemplateBinderFactory>();

            services.Configure(configureOptions);

            // Override the framework's LinkGenerator (DefaultLinkGenerator)
            services.AddSingleton<LinkGenerator, GlobalRouteValuesLinkGenerator>();

            return services;
        }
    }
}
