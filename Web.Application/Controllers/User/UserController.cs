namespace Web.Application.Controllers.User
{
    using Forms;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;



    [Authorize]
    public class UserController : FormControllerBase
    {
        public UserController(
            IFormHandlerFactory formHandlerFactory)
            : base(formHandlerFactory)
        {

        }

        [HttpGet]
        public IActionResult List()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateUserForm form)
        {
            return Form(
                form,
                List,
                List);
        }
    }
}