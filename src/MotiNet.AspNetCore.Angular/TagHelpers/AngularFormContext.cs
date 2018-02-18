﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace MotiNet.AspNetCore.Angular.TagHelpers
{
    public class AngularFormContext
    {
        public string Name { get; set; }

        public string ValidationCondition { get; set; }

        public string ValidationSuccessCssClass { get; set; }

        public string ValidationErrorCssClass { get; set; }

        public bool BootstrapStyle { get; set; }

        public IDictionary<string, TagHelperAttributeList> InputAttributeLists { get; }
            = new Dictionary<string, TagHelperAttributeList>();
    }
}
