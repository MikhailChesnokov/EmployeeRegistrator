namespace Web.Application.Controllers.Error
{
    using Microsoft.AspNetCore.Mvc;
    using ViewModels;



    public class ErrorController : FormControllerBase
    {
        public ErrorController(
            IFormHandlerFactory formHandlerFactory)
            : base(formHandlerFactory) { }

 
        public IActionResult Index()
        {
            return View(new ErrorViewModel{Message = string.Empty});
        }

        public IActionResult Code(int? code = null)
        {
            return View(new StatusCodeViewModel{Code = code ?? 0});
        }

        public IActionResult AccessDenied()
        {
            return View("Index", new ErrorViewModel { Message = "доступ к запрашиваемой странице запрещен" });
        }
    }
}