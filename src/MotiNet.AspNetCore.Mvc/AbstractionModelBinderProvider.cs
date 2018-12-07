using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace MotiNet.AspNetCore.Mvc
{
    // Coppied from ComplexTypeModelBinderProvider
    public class AbstractionModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (context.Metadata.IsComplexType &&
                !context.Metadata.IsCollectionType &&
                (context.Metadata.ModelType.GetTypeInfo().IsInterface ||
                 context.Metadata.ModelType.GetTypeInfo().IsAbstract) &&
                CheckModelType(context))
            {
                var propertyBinders = new Dictionary<ModelMetadata, IModelBinder>();
                for (var i = 0; i < context.Metadata.Properties.Count; i++)
                {
                    var property = context.Metadata.Properties[i];
                    if (!propertyBinders.ContainsKey(property))
                    {
                        propertyBinders.Add(property, context.CreateBinder(property));
                    }
                }

                var loggerFactory = context.Services.GetRequiredService<ILoggerFactory>();
                return CreateBinder(propertyBinders, loggerFactory);
            }

            return null;
        }

        protected virtual bool CheckModelType(ModelBinderProviderContext context)
        {
            return true;
        }

        protected virtual IModelBinder CreateBinder(IDictionary<ModelMetadata, IModelBinder> propertyBinders, ILoggerFactory loggerFactory)
        {
            return new AbstractionModelBinder(propertyBinders, loggerFactory);
        }
    }
}
