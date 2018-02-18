using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace MotiNet.AspNetCore.Compozr
{
    public class CompozrHelper
    {
        private readonly RouteData _routeData;

        public CompozrHelper(IHttpContextAccessor httpContextAccessor)
        {
            _routeData = httpContextAccessor.HttpContext.GetRouteData();
        }

        public bool Compozing => _routeData.Values[RouteBuilderExtensions.CompozrRouteValueName] != null;

        public string CurrentActionCompozrHeadViewComponentName => $"{CurrentActionCompozrViewComponentName}Head";

        public string CurrentActionCompozrBodyViewComponentName => $"{CurrentActionCompozrViewComponentName}Body";

        public string CurrentActionCompozrScriptsViewComponentName => $"{CurrentActionCompozrViewComponentName}Scripts";

        private string CurrentActionCompozrViewComponentName
            => $"{_routeData.Values["controller"]}{_routeData.Values["action"]}Compozr";
    }
}
