using System;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace MotiNet.AspNetCore.Mvc.TagHelpers
{
    [HtmlTargetElement("input", Attributes = PlaceholderAttributeName + "," + ForAttributeName, TagStructure = TagStructure.WithoutEndTag)]
    [HtmlTargetElement("textarea", Attributes = PlaceholderAttributeName + "," + ForAttributeName)]
    public class PlaceholderTagHelper : TagHelper
    {
        protected const string PlaceholderAttributeName = "placeholder";
        private const string ForAttributeName = "asp-for";

        public string Placeholder { get; set; }

        [HtmlAttributeName(ForAttributeName)]
        public ModelExpression For { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (output == null)
            {
                throw new ArgumentNullException(nameof(output));
            }

            string placeholder;
            if (string.IsNullOrWhiteSpace(Placeholder))
            {
                placeholder = For.Metadata.GetDisplayName();
            }
            else if (Placeholder.Contains("{0}"))
            {
                placeholder = Placeholder.Replace("{0}", For.Metadata.GetDisplayName());
            }
            else
            {
                placeholder = Placeholder;
            }
            output.Attributes.SetAttribute(PlaceholderAttributeName, placeholder);
        }
    }
}
