using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Template;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;

namespace MotiNet.AspNetCore.GlobalRouteValuesLinkGenerator
{
    public class GlobalRouteValuesLinkGenerator : DefaultLinkGenerator
    {
        private readonly string[] _globalRouteKeys;

        public GlobalRouteValuesLinkGenerator(
            ParameterPolicyFactory parameterPolicyFactory,
            GlobalRouteValuesLinkGeneratorTemplateBinderFactory binderFactory,
            EndpointDataSource dataSource,
            IOptions<RouteOptions> routeOptions,
            ILogger<DefaultLinkGenerator> logger,
            IServiceProvider serviceProvider,
            IOptions<GlobalRouteValuesLinkGeneratorOptions> options)
            : base(parameterPolicyFactory, binderFactory, dataSource, routeOptions, logger, serviceProvider)
        {
            _globalRouteKeys = options.Value.GlobalRouteKeys ?? Array.Empty<string>();
        }

        public override string GetPathByAddress<TAddress>(
            HttpContext httpContext,
            TAddress address,
            RouteValueDictionary values,
            RouteValueDictionary ambientValues = default,
            PathString? pathBase = default,
            FragmentString fragment = default,
            LinkOptions options = null)
        {
            MigrateGlobalAmbientValues(values, ambientValues);

            return base.GetPathByAddress(httpContext, address, values, ambientValues, pathBase, fragment, options);
        }

        public override string GetUriByAddress<TAddress>(
            HttpContext httpContext,
            TAddress address,
            RouteValueDictionary values,
            RouteValueDictionary ambientValues = default,
            string scheme = default,
            HostString? host = default,
            PathString? pathBase = default,
            FragmentString fragment = default,
            LinkOptions options = null)
        {
            MigrateGlobalAmbientValues(values, ambientValues);

            return base.GetUriByAddress(httpContext, address, values, ambientValues, scheme, host, pathBase, fragment, options);
        }

        private void MigrateGlobalAmbientValues(RouteValueDictionary values, RouteValueDictionary ambientValues)
        {
            foreach (var key in _globalRouteKeys)
            {
                if (ambientValues.ContainsKey(key))
                {
                    values.TryAdd(key, ambientValues[key]);
                }
            }
        }
    }
}
