namespace Web.Application.Controllers
{
    using System;
    using Microsoft.AspNetCore.Mvc;



    public class FormControllerBase : Controller
    {
        private readonly IFormHandlerFactory _formHandlerFactory;



        protected FormControllerBase(
            IFormHandlerFactory formHandlerFactory)
        {
            _formHandlerFactory = formHandlerFactory;
        }



        protected IActionResult Form<TForm>(TForm form, Func<IActionResult> success, Func<IActionResult> failure)
            where TForm : IForm
        {
            if (ModelState.IsValid)
                try
                {
                    _formHandlerFactory.Create<TForm>().Execute(form);

                    return success();
                }
                catch (FormException e)
                {
                    ModelState.AddModelError(string.Empty, e.Message);
                }

            return failure();
        }

        protected IActionResult Form<TForm, TFormResult>(TForm form, Func<TFormResult, IActionResult> success, Func<IActionResult> failure)
            where TForm : IForm
        {
            if (ModelState.IsValid)
                try
                {
                    TFormResult result = _formHandlerFactory.Create<TForm, TFormResult>().Execute(form);

                    return success(result);
                }
                catch (FormException e)
                {
                    ModelState.AddModelError(string.Empty, e.Message);
                }

            return failure();
        }
    }
}