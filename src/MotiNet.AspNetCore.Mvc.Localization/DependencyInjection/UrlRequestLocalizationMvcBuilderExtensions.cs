using MotiNet.AspNetCore.Mvc.Localization;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class UrlRequestLocalizationMvcBuilderExtensions
    {
        public static IMvcBuilder AddUrlRequestLocalization(this IMvcBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.AddRazorPagesOptions(options =>
            {
                options.Conventions.Add(new CultureTemplatePageRouteModelConvention());
            });

            builder.Services.AddCultureTemplatePage();

            return builder;
        }
    }
}
