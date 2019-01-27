namespace Web.Application.Services.ExcelGenerator
{
    using System.Drawing;
    using OfficeOpenXml;
    using OfficeOpenXml.Style;



    internal sealed class WriteContext
    {
        public ExcelPackage Package { get; set; }

        public Color Color { get; set; } = Color.White;

        public object Value { get; set; }

        public (string column, int? row) From { get; set; }
    }
    

    internal static class ExcelExtensions
    {
        internal static WriteContext Write(this ExcelPackage package, object value)
        {
            return new WriteContext
            {
                Package = package,
                Value = value
            };
        }

        internal static WriteContext Fill(this WriteContext context, Color color)
        {
            context.Color = color;

            return context;
        }

        public static WriteContext From(this WriteContext context, string column, int row)
        {
            context.From = (column, row);

            return context;
        }
        
        public static void To(this WriteContext context, string column, int row)
        {
            var range =
                context.Package
                    .Workbook
                    .Worksheets[0]
                    .Cells[$"{context.From.column ?? column}{context.From.row ?? row}:{column}{row}"];
            range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            if (context.Color != Color.White)
            {
                range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                range.Style.Fill.BackgroundColor.SetColor(context.Color);
            }
            range.Merge = true;
            range.Value = context.Value;
        }
    }
}