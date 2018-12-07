using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MotiNet.AspNetCore.Mvc
{
    public class AbstractionJsonModelBinder : IModelBinder
    {
        public virtual async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            using (StreamReader reader = new StreamReader(bindingContext.HttpContext.Request.Body, Encoding.UTF8))
            {
                var body = await reader.ReadToEndAsync();
                Type modelType;
                if (bindingContext.ModelMetadata.IsComplexType &&
                    !bindingContext.ModelMetadata.IsCollectionType &&
                    (bindingContext.ModelType.IsInterface ||
                     bindingContext.ModelType.IsAbstract))
                {
                    var service = bindingContext.HttpContext.RequestServices.GetRequiredService(GetModelAbstractType(bindingContext, body));
                    modelType = service.GetType();
                }
                else
                {
                    modelType = bindingContext.ModelType;
                }
                var model = JsonConvert.DeserializeObject(body, modelType);
                bindingContext.Result = ModelBindingResult.Success(model);
            }
        }

        protected virtual Type GetModelAbstractType(ModelBindingContext bindingContext, string body) => bindingContext.ModelType;
    }
}
