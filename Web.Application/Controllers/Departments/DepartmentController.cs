namespace Web.Application.Controllers.Departments
{
    using System.Collections.Generic;
    using AutoMapper;
    using Domain.Entities.Department;
    using Domain.Entities.User;
    using Domain.Services.Department;
    using Forms;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using ViewModels;


    
    [Authorize]
    public sealed class DepartmentController : FormControllerBase
    {
        private readonly IDepartmentService _departmentService;
        private readonly IMapper _mapper;
        
        
        
        public DepartmentController(
            IFormHandlerFactory formHandlerFactory,
            IAuthorizationService authorizationService,
            IDepartmentService departmentService,
            IMapper mapper)
            : base(
                formHandlerFactory,
                authorizationService)
        {
            _departmentService = departmentService;
            _mapper = mapper;
        }


        
        [HttpGet]
        public IActionResult List()
        {
            if (!RoleIs(Roles.Administrator, Roles.Manager, Roles.SecurityGuard))
                return Forbid();
            
            
            IEnumerable<Department> departments = _departmentService.AllActive();

            IEnumerable<DepartmentViewModel> departmentViewModels = _mapper.Map<IEnumerable<DepartmentViewModel>>(departments);

            return View(departmentViewModels);
        }

        [HttpGet]
        public IActionResult View(int id)
        {
            if (!RoleIs(Roles.Administrator, Roles.Manager, Roles.SecurityGuard))
                return Forbid();

            
            var department = _departmentService.GetById(id);

            if (department is null)
                return NotFound();

            var departmentViewModel = _mapper.Map<DepartmentViewModel>(department);
            
            return View(departmentViewModel);
        }
        
        [HttpGet]
        public IActionResult Create()
        {
            if (!RoleIs(Roles.Administrator))
                return Forbid();
            
            
            return View(new CreateDepartmentForm());
        }

        [HttpPost]
        public IActionResult Create(CreateDepartmentForm form)
        {
            if (!RoleIs(Roles.Administrator))
                return Forbid();
            
            
            return Form(
                form,
                (int id) => this.RedirectToAction(c => c.View(id)),
                () => View(form));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (!RoleIs(Roles.Administrator))
                return Forbid();
            
            
            var department = _departmentService.GetById(id);

            if (department is null)
                return NotFound();

            var form = _mapper.Map<EditDepartmentForm>(department);

            return View(form);
        }

        [HttpPost]
        public IActionResult Edit(EditDepartmentForm form)
        {
            if (!RoleIs(Roles.Administrator))
                return Forbid();

            
            return Form(
                form,
                () => this.RedirectToAction(x => x.View(form.Id.Value)),
                () => View(form));
        }
        
        [HttpPost]
        public IActionResult Delete(DeleteDepartmentForm form)
        {
            if (!RoleIs(Roles.Administrator))
                return Forbid();
            
            
            return Form(
                form,
                () => this.RedirectToAction(x => x.List()),
                () => this.RedirectToAction(x => x.List()));
        }
    }
}