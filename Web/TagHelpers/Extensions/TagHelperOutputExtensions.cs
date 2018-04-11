namespace Web.TagHelpers.Extensions
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Razor.TagHelpers;



    public static class TagHelperOutputExtensions
    {
        public static TagHelperOutput AddLabel(
            this TagHelperOutput output,
            string name,
            string content,
            int width = 3)
        {
            output.Content.AppendHtml(
                $"<label class=\"col-form-label col-sm-{width}\" for=\"{name}\">{content}</label>");

            return output;
        }

        public static TagHelperOutput AddReadOnlyTextInput(
            this TagHelperOutput output,
            string name,
            string value,
            int width = 9)
        {
            output.Content.AppendHtml(
                $"<input type=\"text\" readonly id=\"{name}\" value=\"{value}\" class=\"form-control-plaintext col-sm-{width}\"/>");

            return output;
        }

        public static TagHelperOutput AddTextInput(
            this TagHelperOutput output,
            string name,
            string value,
            string label,
            ViewContext viewContext,
            int width = 6)
        {
            output.Content.AppendHtml(
                $"<input type=\"text\" id=\"{name}\" name=\"{name}\" value=\"{viewContext.ModelState[name]?.AttemptedValue ?? value}\" class=\"form-control col-sm-{width} {(viewContext.ModelState[name]?.Errors?.Count > 0 ? "is-invalid" : "")}\" placeholder=\"Введите {label.ToLower()}...\"/>");

            return output;
        }

        public static TagHelperOutput AddInvalidFeedback(
            this TagHelperOutput output,
            string message,
            int width = 3)
        {
            output.Content.AppendHtml(
                $"<div class=\"invalid-feedback col-sm-{width}\">{message}</div>");

            return output;
        }

        public static TagHelperOutput AddSelect(
            this TagHelperOutput output,
            string name,
            string label,
            IEnumerable<SelectListItem> items,
            string placeholder,
            ViewContext viewContext,
            int width = 6)
        {
            output.Content.AppendHtml(
                $"<select class=\"custom-select col-sm-{width} {(viewContext.ModelState[name]?.Errors?.Count > 0 ? "is-invalid" : "")}\" id=\"{name}\" name=\"{name}\">" +
                $"<option selected>{placeholder}</option>\n" +
                items.Aggregate(string.Empty, (s, item) =>
                                    $"{s}<option value=\"{item.Value}\">{item.Text}</option>\n") +
                "</select>");

            return output;
        }

        public static TagHelperOutput AddFile(
            this TagHelperOutput output,
            string name,
            string label,
            string placeholder,
            string invalidFeedback,
            ViewContext viewContext,
            int width = 6
        )
        {
            output.Content.AppendHtml(
                $"<div class=\"custom-file col-sm-{width}\" id=\"{name}Wrapper\">" +
                $"<input type=\"file\" class=\"custom-file-input {(viewContext.ModelState[name]?.Errors?.Count > 0 ? "is-invalid" : "")}\" id=\"{name}\" name=\"{name}\">" +
                $"<label class=\"custom-file-label\" for=\"customFile\">{placeholder}</label>" +
                $"<div class=\"invalid-feedback\">{invalidFeedback}</div>" +
                "</div>");

            return output;
        }
    }
}