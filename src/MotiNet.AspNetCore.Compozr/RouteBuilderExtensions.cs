using Microsoft.AspNetCore.Routing;

namespace Microsoft.AspNetCore.Builder
{
    public static class RouteBuilderExtensions
    {
        public const string CompozrRouteValueName = "compozing";

        private const string DefaultRouteTemplate = "{controller=Home}/{action=Index}/{id?}";

        public static IRouteBuilder MapCompozrRoute(this IRouteBuilder routeBuilder)
        {
            routeBuilder.MapRoute(
                name: "compozr",
                template: $"{{{CompozrRouteValueName}:regex(compozr)}}/{DefaultRouteTemplate}");

            return routeBuilder;
        }
    }
}
