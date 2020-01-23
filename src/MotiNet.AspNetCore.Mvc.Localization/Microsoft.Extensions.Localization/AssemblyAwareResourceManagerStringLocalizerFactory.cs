using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Reflection;

namespace Microsoft.Extensions.Localization
{
    internal class AssemblyAwareResourceManagerStringLocalizerFactory : ResourceManagerStringLocalizerFactory
    {
        public AssemblyAwareResourceManagerStringLocalizerFactory(
            IOptions<LocalizationOptions> localizationOptions,
            ILoggerFactory loggerFactory)
            : base(localizationOptions, loggerFactory)
        { }

        protected override string GetResourcePrefix(TypeInfo typeInfo, string baseNamespace, string resourcesRelativePath)
        {
            if (typeInfo == null)
            {
                throw new ArgumentNullException(nameof(typeInfo));
            }

            if (string.IsNullOrEmpty(baseNamespace))
            {
                throw new ArgumentNullException(nameof(baseNamespace));
            }

            // Ignore resourcesRelativePath if getting resources from a different assembly
            if (!typeInfo.FullName.StartsWith(baseNamespace))
            {
                return typeInfo.FullName;
            }

            return base.GetResourcePrefix(typeInfo, baseNamespace, resourcesRelativePath);
        }
    }
}
