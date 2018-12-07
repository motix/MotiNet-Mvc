using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.AspNetCore.Mvc.DataAnnotations.Internal;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Localization;
using MotiNet.AspNetCore.Mvc;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class MvcServiceCollectionExtensions
    {
        public static IMvcBuilder AddLocalizedMvc(this IServiceCollection services)
        {
            return AddLocalizedMvc(services, null);
        }

        public static IMvcBuilder AddLocalizedMvc(this IServiceCollection services, Action<MvcOptions> setupAction)
        {
            services.AddLocalization(options => options.ResourcesPath = "Resources")
                    .AddSingleton<IStringLocalizerFactory, AssemblyAwareResourceManagerStringLocalizerFactory>();

            IMvcBuilder builder;
            if (setupAction == null)
            {
                builder = services.AddMvc();
            }
            else
            {
                builder = services.AddMvc(setupAction);
            }
            builder.AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                   .AddDataAnnotationsLocalization();

            services.AddSingleton<IValidationAttributeAdapterProvider, LocalizedValidationAttributeAdapterProvider>();

            services.AddScoped<MvcHelper>();

            return builder;
        }
    }
}
