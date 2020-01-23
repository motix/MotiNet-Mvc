using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Localization;
using MotiNet.AspNetCore.Mvc.Localization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class UrlRequestLocalizationServiceCollectionExtensions
    {
        public static IServiceCollection AddCultureTemplatePage(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddGlobalRouteValuesLinkGenerator(options =>
            {
                options.GlobalRouteKeys = new string[] { CultureTemplatePageRouteModelConvention.CultureTemplateParameter };
            });

            return services;
        }

        public static IServiceCollection AddUrlRequestLocalization(this IServiceCollection services, Func<IMvcBuilder> addMvc)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (addMvc == null)
            {
                throw new ArgumentNullException(nameof(addMvc));
            }

            services.AddLocalization(options => options.ResourcesPath = "Resources")
                    .AddSingleton<IStringLocalizerFactory, AssemblyAwareResourceManagerStringLocalizerFactory>();

            var builder = addMvc();

            builder.AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                   .AddDataAnnotationsLocalization()
                   .AddUrlRequestLocalization();

            return services;
        }

        public static void ConfigureUrlRequestLocalization(
            this IServiceCollection services,
            IList<string> cultures,
            string defaultCulture = default)
        => ConfigureUrlRequestLocalization(
            services,
            cultures.Select(x => new CultureInfo(x)).ToList(),
            defaultCulture == null ? null : new CultureInfo(defaultCulture));

        public static void ConfigureUrlRequestLocalization(
            this IServiceCollection services,
            IList<CultureInfo> cultures,
            CultureInfo defaultCulture = default)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (defaultCulture == null && cultures.Count > 0)
            {
                defaultCulture = cultures.First();
            }

            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new RequestCulture(defaultCulture);
                options.SupportedCultures = cultures;
                options.SupportedUICultures = cultures;

                options.RequestCultureProviders.Insert(0, new UrlRequestCultureProvider() { Options = options });
            });
        }
    }
}
