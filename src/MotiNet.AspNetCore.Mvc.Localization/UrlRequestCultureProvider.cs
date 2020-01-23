using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace MotiNet.AspNetCore.Mvc.Localization
{
    public class UrlRequestCultureProvider : RequestCultureProvider
    {
        public override Task<ProviderCultureResult> DetermineProviderCultureResult(HttpContext httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException(nameof(httpContext));
            }

            var path = httpContext.Request.Path;

            if (string.IsNullOrWhiteSpace(path))
            {
                return NullProviderCultureResult;
            }

            var segments = httpContext.Request.Path.Value.Split('/');

            if (segments.Count() < 2 || string.IsNullOrWhiteSpace(segments[1]))
            {
                return NullProviderCultureResult;
            }

            var culture = segments[1];

            try
            {
                CultureInfo.GetCultureInfo(culture);
            }
            catch (CultureNotFoundException)
            {
                return NullProviderCultureResult;
            }

            var providerResultCulture = new ProviderCultureResult(culture);

            return Task.FromResult(providerResultCulture);
        }
    }
}
