namespace Web.TagHelpers.Extensions
{
    using System.Collections.Generic;
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
            TagBuilder labelTag = new TagBuilder("label")
            {
                Attributes =
                {
                    {"class", $"col-form-label col-sm-{width}"},
                    {"for", name}
                }
            };

            TagBuilder strongContext = new TagBuilder("strong");

            strongContext.InnerHtml.Append(content);

            labelTag.InnerHtml.AppendHtml(strongContext);

            output.Content.AppendHtml(labelTag);

            return output;
        }

        public static TagHelperOutput AddReadOnlyTextInput(
            this TagHelperOutput output,
            string name,
            string value,
            int width = 9)
        {
            TagBuilder readOnlyInputTag = new TagBuilder("input")
            {
                Attributes =
                {
                    {"type", "text"},
                    {"readonly", "readonly"},
                    {"id", name},
                    {"value", value},
                    {"class", $"form-control-plaintext col-sm-{width}"}
                }
            };

            output.Content.AppendHtml(readOnlyInputTag);

            return output;
        }

        public static TagHelperOutput AddTextInput(
            this TagHelperOutput output,
            string name,
            string value,
            string label,
            string placeholder,
            bool isPassword,
            ViewContext viewContext,
            int width = 6)
        {
            TagBuilder inputTag = new TagBuilder("input")
            {
                Attributes =
                {
                    {"type", isPassword ? "password" : "text"},
                    {"id", name},
                    {"name", name},
                    {"value", viewContext.ModelState[name]?.AttemptedValue ?? value ?? string.Empty},
                    {"class", $"form-control col-sm-{width} {(viewContext.ModelState[name]?.Errors?.Count > 0 ? "is-invalid" : string.Empty)}"},
                    {"placeholder", placeholder ?? $"Введите {label.ToLower()} ..."}
                }
            };

            output.Content.AppendHtml(inputTag);

            return output;
        }

        public static TagHelperOutput AddInvalidFeedback(
            this TagHelperOutput output,
            string message,
            int width = 3)
        {
            TagBuilder feedbackTag = new TagBuilder("div")
            {
                Attributes =
                {
                    {"class", $"invalid-feedback col-sm-{width}"}
                }
            };

            feedbackTag.InnerHtml.Append(message);

            output.Content.AppendHtml(feedbackTag);

            return output;
        }

        public static TagHelperOutput AddSelect(
            this TagHelperOutput output,
            string name,
            string label,
            IEnumerable<SelectListItem> items,
            string placeholder,
            long? value,
            ViewContext viewContext,
            int width = 6)
        {
            TagBuilder selectTag = new TagBuilder("select")
            {
                Attributes =
                {
                    {"class", $"custom-select col-sm-{width} {(viewContext.ModelState[name]?.Errors?.Count > 0 ? "is-invalid" : string.Empty)}"},
                    {"id", name},
                    {"name", name}
                }
            };

            SetDefaultOption();

            SetOptions();

            output.Content.AppendHtml(selectTag);

            return output;


            void SetDefaultOption()
            {
                TagBuilder defaultOptioin = new TagBuilder("option");

                defaultOptioin.InnerHtml.Append(placeholder ?? $"Выберите {label.ToLower()}");

                if (value is null)
                    defaultOptioin.Attributes.Add("selected", "selected");

                selectTag.InnerHtml.AppendHtml(defaultOptioin);
            }

            void SetOptions()
            {
                foreach (SelectListItem item in items)
                {
                    TagBuilder option = new TagBuilder("option");

                    option.Attributes.Add("value", item.Value);

                    if (value != null && ((long)value).ToString() == item.Value)
                        option.Attributes.Add("selected", "selected");

                    option.InnerHtml.Append(item.Text);

                    selectTag.InnerHtml.AppendHtml(option);
                }
            }
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
            TagBuilder wrapperTag = new TagBuilder("div")
            {
                Attributes =
                {
                    {"class", $"custom-file col-sm-{width}"},
                    {"id", $"{name}Wrapper"}
                }
            };

            SetInput();

            SetLabel();

            SetFeedback();

            output.Content.AppendHtml(wrapperTag);

            return output;


            void SetInput()
            {
                wrapperTag.InnerHtml.AppendHtml(new TagBuilder("input")
                {
                    Attributes =
                    {
                        {"type", "file"},
                        {"class", $"custom-file-input {(viewContext.ModelState[name]?.Errors?.Count > 0 ? "is-invalid" : string.Empty)}"},
                        {"id", name},
                        {"name", name}
                    }
                });
            }

            void SetLabel()
            {
                TagBuilder labelTag = new TagBuilder("label")
                {
                    Attributes =
                    {
                        {"class", "custom-file-label"},
                        {"for", name}
                    }
                };

                labelTag.InnerHtml.Append(placeholder ?? "Выберите файл ...");

                wrapperTag.InnerHtml.AppendHtml(labelTag);
            }

            void SetFeedback()
            {
                TagBuilder feedbackTag = new TagBuilder("div")
                {
                    Attributes =
                    {
                        {"class", "invalid-feedback"}
                    }
                };

                feedbackTag.InnerHtml.Append(invalidFeedback ?? string.Empty);

                wrapperTag.InnerHtml.AppendHtml(feedbackTag);
            }
        }
    }
}