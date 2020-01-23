using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace MotiNet.AspNetCore.Mvc.Localization
{
    public class CultureTemplatePageRouteModelConvention : IPageRouteModelConvention
    {
        public const string CultureTemplateParameter = "culture";

        public void Apply(PageRouteModel model)
        {
            var selectorCount = model.Selectors.Count;
            for (var i = 0; i < selectorCount; i++)
            {
                var selector = model.Selectors[i];
                if (selector.AttributeRouteModel.Template != "Index" && !selector.AttributeRouteModel.Template.EndsWith("/Index"))
                {
                    model.Selectors.Add(new SelectorModel
                    {
                        AttributeRouteModel = new AttributeRouteModel
                        {
                            Order = -1,
                            Template = AttributeRouteModel.CombineTemplates($"{{{CultureTemplateParameter}?}}", selector.AttributeRouteModel.Template),
                        }
                    });
                }
            }
        }
    }
}
