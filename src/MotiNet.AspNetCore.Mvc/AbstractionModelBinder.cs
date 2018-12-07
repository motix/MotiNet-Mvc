using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace MotiNet.AspNetCore.Mvc
{
    public class AbstractionModelBinder : ComplexTypeModelBinder
    {
        public AbstractionModelBinder(IDictionary<ModelMetadata, IModelBinder> propertyBinders, ILoggerFactory loggerFactory)
            : base(propertyBinders, loggerFactory)
        { }

        protected override object CreateModel(ModelBindingContext bindingContext)
            => bindingContext.HttpContext.RequestServices.GetRequiredService(GetModelAbstractType(bindingContext));

        protected virtual Type GetModelAbstractType(ModelBindingContext bindingContext) => bindingContext.ModelType;
    }
}
