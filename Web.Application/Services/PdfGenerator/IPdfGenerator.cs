namespace Web.Application.Services.PdfGenerator
{
    using System.IO;
    using System.Threading.Tasks;



    public interface IPdfGenerator
    {
        Task<Stream> GenerateAsync(string htmlContent);
    }
}