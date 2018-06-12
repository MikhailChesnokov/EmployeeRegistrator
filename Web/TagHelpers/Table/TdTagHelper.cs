namespace Web.TagHelpers.Table
{
    using System.Collections.Concurrent;
    using Microsoft.AspNetCore.Razor.TagHelpers;



    [HtmlTargetElement(
        TagName,
        Attributes = RequiredAttributes,
        ParentTag = ParentTag,
        TagStructure = TagStructure.NormalOrSelfClosing)]
    public class TdTagHelper : TagHelper
    {
        private const string
            TagName = "td",
            OutputTagName = TagName,
            ParentTag = "tr",
            RequiredAttributes = "rowspan, rowspan-id";



        private ConcurrentDictionary<string, string> _rowspanIds;



        public int Rowspan { get; set; }

        public string RowspanId { get; set; }



        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            _rowspanIds = context.Items[nameof(_rowspanIds)] as ConcurrentDictionary<string, string>;

            lock (_rowspanIds)
            {
                if (_rowspanIds.ContainsKey(RowspanId))
                {
                    output.SuppressOutput();
                }
                else
                {
                    _rowspanIds.TryAdd(RowspanId, string.Empty);

                    output.TagName = OutputTagName;
                    output.TagMode = TagMode.StartTagAndEndTag;
                    output.Attributes.Add(nameof(Rowspan).ToLower(), Rowspan);
                }
            }
        }
    }
}