namespace Web.Application.Controllers.User
{
    using System.Collections.Generic;
    using AutoMapper;
    using Domain.Entities.User;
    using Domain.Services.User;
    using Forms;
    using Infrastructure.Extensions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using ViewModels;



    [Authorize]
    public sealed class UserController : FormControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;



        public UserController(
            IFormHandlerFactory formHandlerFactory,
            IUserService userService,
            IMapper mapper,
            IAuthorizationService authorizationService)
            : base(
                formHandlerFactory,
                authorizationService)
        {
            _userService = userService;
            _mapper = mapper;
        }



        [HttpGet]
        public IActionResult List()
        {
            if (!RoleIs(Roles.Administrator)) return Forbid();


            IEnumerable<User> users = _userService.GetAllActive();

            IEnumerable<UserViewModel> userViewModels = _mapper.Map<IEnumerable<UserViewModel>>(users);

            return View(userViewModels);
        }

        [HttpGet]
        public IActionResult View(int id)
        {
            if (!RoleIs(Roles.Administrator)) return Forbid();


            var user = _userService.GetById(id);

            if (user is null)
                return NotFound();

            var userViewModel = _mapper.Map<UserViewModel>(user);

            return View(userViewModel);
        }
        
        [HttpGet]
        public IActionResult Create()
        {
            if (!RoleIs(Roles.Administrator)) return Forbid();


            CreateUserForm form = new CreateUserForm();

            SetRoles(form);

            return View(form);
        }

        [HttpPost]
        public IActionResult Create(CreateUserForm form)
        {
            if (!RoleIs(Roles.Administrator)) return Forbid();


            return Form(
                form,
                (int userId) => this.RedirectToAction(x => x.List()),
                () =>
                {
                    SetRoles(form);

                    return View(form);
                });
        }

        private void SetRoles(CreateUserForm form, bool includeAll = false)
        {
            List<SelectListItem> items = new List<SelectListItem>();

            if (includeAll)
            {
                items.Add(new SelectListItem
                {
                    Value = null,
                    Text = "Все роли",
                    Group = default,
                    Selected = false,
                    Disabled = false
                });
            }

            items.AddRange(typeof(Roles).ToSelectList());

            form.Roles = items;
        }
    }
}