namespace Web.TagHelpers
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Microsoft.AspNetCore.Razor.TagHelpers;



    [HtmlTargetElement("root-errors", TagStructure = TagStructure.WithoutEndTag)]
    public class RootErrorsTagHelper : TagHelper
    {
        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }



        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (ViewContext.ModelState.Root?.Errors?.Count > 0)
            {
                output.TagName = "div";
                output.Attributes.Add("class", "my-4");
                output.TagMode = TagMode.StartTagAndEndTag;
                output.Content.AppendHtml(
                    ViewContext.ModelState.Root.Errors.Aggregate(
                        string.Empty, (_, error) => $"<div class=\"alert alert-danger\" role=\"alert\">{error.ErrorMessage}</div>\n"));
            }
            else
                output.SuppressOutput();
        }
    }
}