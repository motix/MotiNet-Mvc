using Microsoft.AspNetCore.Routing;

namespace Microsoft.AspNetCore.Builder
{
    public static class RouteBuilderExtensions
    {
        private const string DefaultRouteTemplate = "{controller=Home}/{action=Index}/{id?}";

        public static IRouteBuilder MapDefaultRoute(this IRouteBuilder routeBuilder)
        {
            routeBuilder.MapRoute(
                name: "default",
                template: DefaultRouteTemplate);

            return routeBuilder;
        }
    }
}
