namespace Web.TagHelpers
{
    using Microsoft.AspNetCore.Razor.TagHelpers;



    public class ViewGroupTagHelper : TagHelper
    {
        public string Name { get; set; }

        public string Value { get; set; }

        public string Label { get; set; }



        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Attributes.Add("class", "form-group row");
            output.Content.AppendHtml(
                $"<label class=\"col-form-label col-sm-3\" for=\"{Name}\">{Label}</label>" +
                $"<input type=\"text\" readonly class=\"form-control-plaintext col-sm-9\" id=\"{Name}\" value=\"{Value}\">");
        }
    }
}