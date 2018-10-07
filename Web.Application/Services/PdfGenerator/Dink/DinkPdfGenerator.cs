namespace Web.Application.Services.PdfGenerator.Dink
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using DinkToPdf;
    using DinkToPdf.Contracts;



    public sealed class DinkPdfGenerator : IPdfGenerator
    {
        private readonly IConverter _converter;
        private readonly DinkSettings _settings;


        public DinkPdfGenerator(IConverter converter, DinkSettings settings)
        {
            _converter = converter ?? throw new ArgumentNullException(nameof(converter));
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }



        public async Task<Stream> GenerateAsync(string htmlContent)
        {
            var doc = new HtmlToPdfDocument
            {
                GlobalSettings =
                {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Portrait,
                    PaperSize = PaperKind.A4
                },
                Objects =
                {
                    new ObjectSettings
                    {
                        PagesCount = true,
                        HtmlContent = htmlContent,
                        WebSettings =
                        {
                            DefaultEncoding = "utf-8",
                            UserStyleSheet = _settings.CssPath
                        }
                    }
                }
            };

            byte[] fileBody = _converter.Convert(doc);

            return await Task.FromResult(new MemoryStream(fileBody));
        }
    }
}