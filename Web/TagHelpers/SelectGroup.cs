namespace Web.TagHelpers
{
    using System.Collections.Generic;
    using Extensions;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Microsoft.AspNetCore.Razor.TagHelpers;



    [HtmlTargetElement(
        "select-group",
        Attributes = "name, label, items",
        TagStructure = TagStructure.NormalOrSelfClosing)]
    public class SelectGroupTagHelper : TagHelper
    {
        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        public string Name { get; set; }

        public string Label { get; set; }

        public string Placeholder { get; set; }

        public long? Value { get; set; }

        public string InvalidFeedback { get; set; }

        public bool Grouping { get; set; }

        public IEnumerable<SelectListItem> Items { get; set; }



        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Attributes.Add("class", "form-group row");
            output.TagMode = TagMode.StartTagAndEndTag;
            output
                .AddLabel(Name, Label)
                .AddSelect(Name, Label, Items, Placeholder, Value, Grouping ,ViewContext)
                .AddInvalidFeedback(InvalidFeedback ?? $"{Label} не выбран");
        }
    }
}