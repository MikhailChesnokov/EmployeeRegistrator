namespace Web.Application.Services.ExcelGenerator
{
    using System.IO;
    using System.Threading.Tasks;
    using Controllers.Registration.ViewModels;



    public interface IExcelGenerator
    {
        Task<Stream> GenerateAsync(RegistrationsViewModel registrationsViewModel);
    }
}