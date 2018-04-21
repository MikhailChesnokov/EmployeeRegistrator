namespace Web.Application.Controllers.Account
{
    using Domain.Entities.User;
    using Domain.Infrastructure.Authentication;
    using Employee;
    using Forms;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;



    public class AccountController : FormControllerBase
    {
        private readonly IAuthenticationService<User> _authenticationService;



        public AccountController(
            IFormHandlerFactory formHandlerFactory,
            IAuthenticationService<User> authenticationService)
            : base(formHandlerFactory)
        {
            _authenticationService = authenticationService;
        }



        [HttpGet]
        [AllowAnonymous]
        public IActionResult SignIn()
        {
            return View(new SignInForm());
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult SignIn(SignInForm form, [FromQuery] string returnUrl = null)
        {
            return Form(
                form,
                () => returnUrl != null
                    ? (IActionResult)Redirect(returnUrl)
                    : this.RedirectToAction<EmployeeController>(c => c.List()),
                () => View(form));
        }

        [HttpGet]
        [Authorize]
        public IActionResult SignOut()
        {
            _authenticationService.SignOut();

            return this.RedirectToAction(c => c.SignIn());
        }

        [HttpGet]
        [Authorize]
        public IActionResult SignUp()
        {
            return View(new SignUpForm());
        }

        [HttpPost]
        [Authorize]
        public IActionResult SignUp(SignUpForm form)
        {
            return Form(
                form,
                () => this.RedirectToAction<EmployeeController>(c => c.List()),
                () => View(form));
        }
    }
}