namespace Web.TagHelpers
{
    using Microsoft.AspNetCore.Razor.TagHelpers;



    public class InputGroupTagHelper : TagHelper
    {
        public string Name { get; set; }

        public string Value { get; set; }

        public string Label { get; set; }

        public string InvalidFeedback { get; set; }

        public bool IsInvalid { get; set; }



        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Attributes.Add("class", "form-group row");
            output.Content.AppendHtml(
                $"<label class=\"col-form-label col-sm-3\" for=\"{Name}\">{Label}</label>" +
                $"<input type=\"text\" class=\"form-control col-sm-6 {(IsInvalid ? "is-invalid" : "")}\" id=\"{Name}\" name=\"{Name}\" placeholder=\"Введите {Label.ToLower()}...\" value=\"{Value}\">" +
                $"<div class=\"invalid-feedback col-sm-3\">{InvalidFeedback}</div>");
        }
    }
}