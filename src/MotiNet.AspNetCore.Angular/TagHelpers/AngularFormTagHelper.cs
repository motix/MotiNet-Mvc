﻿using System;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace MotiNet.AspNetCore.Angular.TagHelpers
{
    [HtmlTargetElement("form", Attributes = NameAttributeName)]
    [HtmlTargetElement("form", Attributes = NameAttributeName + "," + BootstrapStyleAttributeName)]
    [HtmlTargetElement("form", Attributes = NameAttributeName + "," + ValidationSuccessCssClassAttributeName + "," + ValidationErrorCssClassAttributeName)]
    public class AngularFormTagHelper : TagHelper
    {
        internal const string NameAttributeName = "pn-ng-name";
        internal const string BootstrapStyleAttributeName = "bootstrap-style";
        internal const string ValidationSuccessCssClassAttributeName = "validation-success-css-class";
        internal const string ValidationErrorCssClassAttributeName = "validation-error-css-class";

        [HtmlAttributeName(NameAttributeName)]
        public string Name { get; set; }

        public string ValidationCondition { get; set; }

        public string ValidationSuccessCssClass { get; set; }

        public string ValidationErrorCssClass { get; set; }

        public override void Init(TagHelperContext context)
        {
            context.Items.Add(typeof(AngularFormTagHelper), new AngularFormContext {
                Name = Name,
                ValidationCondition = ValidationCondition,
                BootstrapStyle = context.AllAttributes.ContainsName(BootstrapStyleAttributeName),
                ValidationSuccessCssClass = ValidationSuccessCssClass,
                ValidationErrorCssClass = ValidationErrorCssClass
            });
        }

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

            output.Attributes.RemoveAll(BootstrapStyleAttributeName);
            output.Attributes.RemoveAll(ValidationSuccessCssClassAttributeName);
            output.Attributes.RemoveAll(ValidationErrorCssClassAttributeName);

            output.Attributes.SetAttribute("name", Name);
            output.Attributes.SetAttribute("novalidate", null);
        }
    }
}
