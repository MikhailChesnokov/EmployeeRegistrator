namespace Web.Modules
{
    using Application.Services.HtmlLayoutGenerator;
    using Application.Services.PdfGenerator;
    using Application.Services.PdfGenerator.Dink;
    using Autofac;
    using DinkToPdf;
    using DinkToPdf.Contracts;
    using Microsoft.Extensions.Configuration;



    public class DocumentsModule : ConfiguredModule
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<RazorHtmlLayoutGenerator>()
                .As<IRazorHtmlLayoutGenerator>()
                .SingleInstance();
            
            builder
                .RegisterInstance(new SynchronizedConverter(new PdfTools()))
                .As<IConverter>()
                .SingleInstance();
            
            builder
                .RegisterType<DinkPdfGenerator>()
                .As<IPdfGenerator>()
                .WithParameter("settings", ConfigurationRoot.GetSection("Documents:Pdf").Get<DinkSettings>())
                .SingleInstance();
        }
    }
}