namespace Web.TagHelpers.Table
{
    using System.Collections.Concurrent;
    using Microsoft.AspNetCore.Razor.TagHelpers;


    [HtmlTargetElement(
        TagName,
        Attributes = RequiredAttributes,
        ParentTag = ParentTag,
        TagStructure = TagStructure.NormalOrSelfClosing)]
    public class TbodyTagHelper : TagHelper
    {
        private const string
            TagName = "tbody",
            OutputTagName = TagName,
            ParentTag = "table",
            RequiredAttributes = "";



        private readonly ConcurrentDictionary<string, string> _rowspanIds = new ConcurrentDictionary<string, string>();


        
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = OutputTagName;
            output.TagMode = TagMode.StartTagAndEndTag;

            context.Items.Add(nameof(_rowspanIds), _rowspanIds);
        }
    }
}