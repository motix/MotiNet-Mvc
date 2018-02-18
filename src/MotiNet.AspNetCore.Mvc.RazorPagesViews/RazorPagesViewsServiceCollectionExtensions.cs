using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.FileProviders;
using MotiNet.AspNetCore.Mvc;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class RazorPagesViewsServiceCollectionExtensions
    {
        public static IServiceCollection AddRazorPagesViews(this IServiceCollection services)
        {
            services.Configure<RazorViewEngineOptions>(options => {
                options.FileProviders.Add(new EmbeddedFileProvider(RazorPagesViews.Assembly, RazorPagesViews.Namespace));
            });

            return services;
        }
    }
}
