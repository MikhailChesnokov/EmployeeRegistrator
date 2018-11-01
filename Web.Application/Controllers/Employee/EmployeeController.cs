namespace Web.Application.Controllers.Employee
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using Domain.Entities.Department;
    using Domain.Entities.Employee;
    using Domain.Entities.User;
    using Domain.Services.Department;
    using Domain.Services.Employee;
    using Forms;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using ViewModels;



    [Authorize]
    public class EmployeeController : FormControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IDepartmentService _departmentService;
        private readonly IMapper _mapper;



        public EmployeeController(
            IFormHandlerFactory formHandlerFactory,
            IMapper mapper,
            IEmployeeService employeeService,
            IAuthorizationService authorizationService,
            IDepartmentService departmentService)
            : base(
                formHandlerFactory,
                authorizationService)
        {
            _mapper = mapper;
            _employeeService = employeeService;
            _departmentService = departmentService;
        }



        [HttpGet]
        public IActionResult List()
        {
            if (!RoleIs(Roles.Administrator, Roles.Manager, Roles.SecurityGuard)) return Forbid();


            IEnumerable<Employee> employees = _employeeService.AllActive();

            IEnumerable<EmployeeViewModel> employeeViewModels = _mapper.Map<IEnumerable<EmployeeViewModel>>(employees);

            return View(employeeViewModels);
        }

        [HttpGet]
        public IActionResult Registration()
        {
            if (!RoleIs(Roles.SecurityGuard)) return Forbid();


            return View();
        }

        public IActionResult RegistrationAjax()
        {
            if (!RoleIs(Roles.SecurityGuard)) return Forbid();


            IEnumerable<Employee> employees = _employeeService.AllActive();

            IEnumerable<EmployeeViewModel> employeeViewModels = _mapper.Map<IEnumerable<EmployeeViewModel>>(employees);

            return PartialView(employeeViewModels);
        }

        [HttpGet]
        public IActionResult View(int id)
        {
            if (!RoleIs(Roles.Administrator, Roles.Manager)) return Forbid();


            Employee employee = _employeeService.GetById(id);

            EmployeeViewModel employeeViewModel = _mapper.Map<EmployeeViewModel>(employee);

            return View(employeeViewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (!RoleIs(Roles.Administrator)) return Forbid();

            
            var departments = _departmentService.AllActive();

            return View(new CreateEmployeeForm{Departments = GetDepartmentsItems(departments)});
        }

        [HttpPost]
        public IActionResult Create(CreateEmployeeForm form)
        {
            if (!RoleIs(Roles.Administrator)) return Forbid();


            return Form(
                form,
                (int employeeId) => this.RedirectToAction(c => c.View(employeeId)),
                () =>
                {
                    var departments = _departmentService.AllActive();
                    
                    form.Departments = GetDepartmentsItems(departments, form.DepartmentId);
                    
                    return View(form);
                });
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (!RoleIs(Roles.Administrator)) return Forbid();


            Employee employee = _employeeService.GetById(id);

            EditEmployeeForm form = _mapper.Map<EditEmployeeForm>(employee);

            var departments = _departmentService.AllActive();

            form.Departments = GetDepartmentsItems(departments, form.DepartmentId);

            return View(form);
        }

        [HttpPost]
        public IActionResult Edit(EditEmployeeForm form)
        {
            if (!RoleIs(Roles.Administrator)) return Forbid();


            return Form(
                form,
                () => this.RedirectToAction(c => c.View(form.Id)),
                () =>
                {
                    var departments = _departmentService.AllActive();
                    
                    form.Departments = GetDepartmentsItems(departments, form.DepartmentId);
                    
                    return View(form);
                });
        }

        [HttpPost]
        public IActionResult Delete(DeleteEmployeeForm form)
        {
            if (!RoleIs(Roles.Administrator)) return Forbid();


            return Form(
                form,
                () => this.RedirectToAction(c => c.List()),
                () => this.RedirectToAction(c => c.View(form.Id)));
        }


        private IEnumerable<SelectListItem> GetDepartmentsItems(IEnumerable<Department> departments, int? selectedId = null)
        {
            return departments.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString(),
                Selected = selectedId != null && selectedId == x.Id,
                Disabled = false,
                Group = default
            });
        }
    }
}