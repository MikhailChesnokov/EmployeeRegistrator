namespace Web.Application.Controllers.User
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using AutoMapper;
    using Domain.Entities.User;
    using Domain.Services.User;
    using Forms;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using ViewModels;



    [Authorize]
    public class UserController : FormControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;



        public UserController(
            IFormHandlerFactory formHandlerFactory,
            IUserService userService,
            IMapper mapper)
            : base(formHandlerFactory)
        {
            _userService = userService;
            _mapper = mapper;
        }



        [HttpGet]
        public IActionResult List()
        {
            IEnumerable<User> users = _userService.GetAllActive();

            IEnumerable<UserViewModel> userViewModels = _mapper.Map<IEnumerable<UserViewModel>>(users);

            return View(userViewModels);
        }

        [HttpGet]
        public IActionResult Create()
        {
            CreateUserForm form = new CreateUserForm();

            SetRoles(form);

            return View(form);
        }

        [HttpPost]
        public IActionResult Create(CreateUserForm form)
        {
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

            items.AddRange(Enum.GetNames(typeof(Roles)).Select(x => new SelectListItem
            {
                Value =
                    Enum
                        .Parse<Roles>(x)
                        .ToString(),
                Text =
                    typeof(Roles)
                        .GetField(x)
                        .GetCustomAttributes(false)
                        .OfType<DisplayAttribute>()
                        .Single()
                        .Name,
                Group = default,
                Selected = false,
                Disabled = false
            }));

            form.Roles = items;
        }
    }
}