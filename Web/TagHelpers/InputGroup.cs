namespace Web.TagHelpers
{
    using Extensions;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Microsoft.AspNetCore.Razor.TagHelpers;



    [HtmlTargetElement(
        "input-group",
        Attributes = "name, label",
        TagStructure = TagStructure.NormalOrSelfClosing)]
    public class InputGroupTagHelper : TagHelper
    {
        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        public string Name { get; set; }

        public string Label { get; set; }

        public string InvalidFeedback { get; set; }

        public string Placeholder { get; set; }

        public string Value { get; set; }

        public bool IsPassword { get; set; }



        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Attributes.Add("class", "form-group row");
            output.TagMode = TagMode.StartTagAndEndTag;
            output
                .AddLabel(Name, Label)
                .AddTextInput(Name, Value, Label, Placeholder, IsPassword, ViewContext)
                .AddInvalidFeedback(InvalidFeedback ?? $"Некорректный {Label.ToLower()}");
        }
    }
}