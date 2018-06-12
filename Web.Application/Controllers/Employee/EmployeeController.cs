namespace Web.Application.Controllers.Employee
{
    using System.Collections.Generic;
    using AutoMapper;
    using Domain.Entities.Employee;
    using Domain.Entities.User;
    using Domain.Services.Employee;
    using Forms;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using ViewModels;



    [Authorize]
    public class EmployeeController : FormControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;



        public EmployeeController(
            IFormHandlerFactory formHandlerFactory,
            IMapper mapper,
            IEmployeeService employeeService,
            IAuthorizationService authorizationService)
            : base(
                formHandlerFactory,
                authorizationService)
        {
            _mapper = mapper;
            _employeeService = employeeService;
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


            return View(new CreateEmployeeForm());
        }

        [HttpPost]
        public IActionResult Create(CreateEmployeeForm form)
        {
            if (!RoleIs(Roles.Administrator)) return Forbid();


            return Form(
                form,
                (int employeeId) => this.RedirectToAction(c => c.View(employeeId)),
                () => View(form));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (!RoleIs(Roles.Administrator)) return Forbid();


            Employee employee = _employeeService.GetById(id);

            EditEmployeeForm editEmployeeForm = _mapper.Map<EditEmployeeForm>(employee);

            return View(editEmployeeForm);
        }

        [HttpPost]
        public IActionResult Edit(EditEmployeeForm form)
        {
            if (!RoleIs(Roles.Administrator)) return Forbid();


            return Form(
                form,
                () => this.RedirectToAction(c => c.View(form.Id)),
                () => View(form));
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
    }
}