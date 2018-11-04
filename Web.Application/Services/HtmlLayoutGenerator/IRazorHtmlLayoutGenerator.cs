namespace Web.Application.Services.HtmlLayoutGenerator
{
    using System.Threading.Tasks;



    public interface IRazorHtmlLayoutGenerator
    {
        Task<string> RenderAsync(string viewName, dynamic model);
    }
}