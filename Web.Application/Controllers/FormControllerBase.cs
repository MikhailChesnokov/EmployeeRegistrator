namespace Web.Application.Controllers
{
    using System;
    using Authorization.Requirements;
    using Domain.Entities.User;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;



    public class FormControllerBase : Controller
    {
        private readonly IFormHandlerFactory _formHandlerFactory;
        private readonly IAuthorizationService _authorizationService;



        protected FormControllerBase(
            IFormHandlerFactory formHandlerFactory,
            IAuthorizationService authorizationService)
        {
            _formHandlerFactory = formHandlerFactory;
            _authorizationService = authorizationService;
        }


        
        protected bool RoleIs(params Role[] role)
        {
            return _authorizationService
                   .AuthorizeAsync(
                       User,
                       role,
                       new RoleRequirement())
                   .Result
                   .Succeeded;
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