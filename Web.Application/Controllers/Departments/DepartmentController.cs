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



    public class DepartmentController : FormControllerBase
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
            if (!RoleIs(Roles.Administrator, Roles.Manager, Roles.SecurityGuard)) return Forbid();
            
            
            IEnumerable<Department> departments = _departmentService.AllActive();

            IEnumerable<DepartmentViewModel> departmentViewModels = _mapper.Map<IEnumerable<DepartmentViewModel>>(departments);

            return View(departmentViewModels);
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (!RoleIs(Roles.Administrator)) return Forbid();
            
            
            return View(new CreateDepartmentForm());
        }

        [HttpPost]
        public IActionResult Create(CreateDepartmentForm form)
        {
            if (!RoleIs(Roles.Administrator)) return Forbid();
            
            
            return Form(
                form,
                (int departmentId) => this.RedirectToAction(c => c.List()),
                () => View(form));
        }
    }
}