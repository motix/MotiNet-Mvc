using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using MotiNet.AspNetCore.Mvc.TagHelpers;

namespace MotiNet.AspNetCore.Angular.TagHelpers
{
    [HtmlTargetElement("input", Attributes = PlaceholderAttributeName + "," + AngularForAttributeName, TagStructure = TagStructure.WithoutEndTag)]
    [HtmlTargetElement("textarea", Attributes = PlaceholderAttributeName + "," + AngularForAttributeName)]
    public class AngularPlaceholderTagHelper : PlaceholderTagHelper
    {
        private const string AngularForAttributeName = "pn-ng-for";

        [HtmlAttributeName(AngularForAttributeName)]
        public ModelExpression AngularFor
        {
            get { return For; }
            set { For = value; }
        }
    }
}
