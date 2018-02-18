using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using System;
using System.Globalization;
using System.Linq;

namespace Microsoft.AspNetCore.Builder
{
    public static class MvcApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseLocalization(this IApplicationBuilder app, string defaultCulture, params string[] supportedCultures)
        {
            if (string.IsNullOrWhiteSpace(defaultCulture))
            {
                throw new ArgumentNullException(nameof(defaultCulture));
            }

            var cultures = supportedCultures.Select(x => new CultureInfo(x)).ToArray();

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(defaultCulture),
                SupportedCultures = cultures,
                SupportedUICultures = cultures
            });

            return app;
        }
    }
}
