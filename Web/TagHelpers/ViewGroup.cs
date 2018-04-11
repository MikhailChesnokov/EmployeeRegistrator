namespace Web.TagHelpers
{
    using Extensions;
    using Microsoft.AspNetCore.Razor.TagHelpers;



    [HtmlTargetElement(
        "view-group",
        Attributes = "name, label, value",
        TagStructure = TagStructure.NormalOrSelfClosing)]
    public class ViewGroupTagHelper : TagHelper
    {
        public string Name { get; set; }

        public string Value { get; set; }

        public string Label { get; set; }



        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Attributes.SetAttribute("class", "form-group row");
            output.TagMode = TagMode.StartTagAndEndTag;
            output
                .AddLabel(Name, Label)
                .AddReadOnlyTextInput(Name, Value);
        }
    }
}