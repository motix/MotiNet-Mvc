using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.AspNetCore.Mvc.DataAnnotations.Internal;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Localization;
using MotiNet.AspNetCore.Mvc;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class MvcServiceCollectionExtensions
    {
        public static IServiceCollection AddLocalizedMvc(this IServiceCollection services)
        {
            services.AddLocalization(options => options.ResourcesPath = "Resources")
                    .AddSingleton<IStringLocalizerFactory, AssemblyAwareResourceManagerStringLocalizerFactory>();

            services.AddMvc()
                    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                    .AddDataAnnotationsLocalization();

            services.AddSingleton<IValidationAttributeAdapterProvider, LocalizedValidationAttributeAdapterProvider>();

            services.AddScoped<MvcHelper>();

            return services;
        }
    }
}
