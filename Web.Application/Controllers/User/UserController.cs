﻿namespace Web.Application.Controllers.User
{
    using System.Collections.Generic;
    using System.Linq;
    using Authorization.UserProviders;
    using AutoMapper;
    using Domain.Entities.Entrance;
    using Domain.Entities.User;
    using Domain.Services.Entrance;
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
        private readonly IEntranceService _entranceService;
        private readonly IMapper _mapper;
        private readonly IUserProvider<User> _userProvider;


        public UserController(
            IFormHandlerFactory formHandlerFactory,
            IUserService userService,
            IMapper mapper,
            IAuthorizationService authorizationService,
            IEntranceService entranceService,
            IUserProvider<User> userProvider)
            : base(
                formHandlerFactory,
                authorizationService)
        {
            _userService = userService;
            _mapper = mapper;
            _entranceService = entranceService;
            _userProvider = userProvider;
        }


        [HttpGet]
        public IActionResult List()
        {
            if (!RoleIs(Role.Administrator)) return Forbid();


            var users = _userService.GetAllActive();

            var userViewModels = _mapper.Map<IEnumerable<UserViewModel>>(users);

            return View(userViewModels);
        }

        [HttpGet]
        public IActionResult View(int id)
        {
            if (!RoleIs(Role.Administrator)) return Forbid();


            var user = _userService.GetById(id);

            if (user is null)
                return NotFound();

            var userViewModel = _mapper.Map<UserViewModel>(user);

            return View(userViewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (!RoleIs(Role.Administrator)) return Forbid();


            var form = new CreateUserForm
            {
                Entrances = GetEntranceItems(_entranceService.AllActive())
            };

            SetRoles(form);

            return View(form);
        }

        [HttpPost]
        public IActionResult Create(CreateUserForm form)
        {
            if (!RoleIs(Role.Administrator)) return Forbid();


            return Form(
                form,
                (int userId) => this.RedirectToAction(x => x.List()),
                () =>
                {
                    SetRoles(form);

                    form.Entrances = GetEntranceItems(_entranceService.AllActive(), form.EntranceId);

                    return View(form);
                });
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (!RoleIs(Role.Administrator))
                return Forbid();

            var user = _userService.GetById(id);

            if (user is null)
                return NotFound();

            var form = _mapper.Map<EditUserForm>(user);

            form.Entrances = GetEntranceItems(_entranceService.AllActive());
            
            SetRoles(form, false, user.Role.ToString());

            return View(form);
        }
        
        [HttpPost]
        public IActionResult Edit(EditUserForm form)
        {
            if (!RoleIs(Role.Administrator))
                return Forbid();
            
            return Form(
                form,
                () => this.RedirectToAction(x => x.View(form.Id)),
                () =>
                {
                    var user = _userService.GetById(form.Id);
                    
                    SetRoles(form, false, user.Role.ToString());

                    form.Entrances = GetEntranceItems(_entranceService.AllActive(), form.EntranceId);

                    return View(form);
                });
        }
        
        [HttpPost]
        public IActionResult Delete(DeleteUserForm form)
        {
            if (!RoleIs(Role.Administrator))
                return Forbid();
            
            return Form(
                form,
                () => this.RedirectToAction(x => x.List()),
                () => this.RedirectToAction(x => x.List()));
        }

        private static void SetRoles(CreateUserForm form, bool includeAll = false)
        {
            var items = new List<SelectListItem>();

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

            items.AddRange(typeof(Role).ToSelectList());

            form.Roles = items;
        }
        
        private static void SetRoles(EditUserForm form, bool includeAll = false, string role = null)
        {
            var items = new List<SelectListItem>();

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

            items.AddRange(typeof(Role).ToSelectList(role));

            form.Roles = items;
        }

        private static IEnumerable<SelectListItem> GetEntranceItems(IEnumerable<Entrance> entrances, int? selectedId = null)
        {
            return
                from buildingEntrances in entrances.GroupBy(x => x.Building)
                let building = new SelectListGroup
                {
                    Name = buildingEntrances.Key.Address
                }
                from entrance in buildingEntrances
                select new SelectListItem
                {
                    Text = entrance.Name,
                    Value = entrance.Id.ToString(),
                    Selected = selectedId != null && selectedId == entrance.Id,
                    Disabled = false,
                    Group = building
                };
        }
    }
}