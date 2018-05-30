namespace Web.Application.Controllers.Employee
{
    using System.Collections.Generic;
    using AutoMapper;
    using Domain.Entities.Employee;
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
            IEmployeeService employeeService)
            : base(formHandlerFactory)
        {
            _mapper = mapper;
            _employeeService = employeeService;
        }



        [HttpGet]
        public IActionResult List()
        {
            IEnumerable<Employee> employees = _employeeService.AllActive();

            IEnumerable<EmployeeViewModel> employeeViewModels = _mapper.Map<IEnumerable<EmployeeViewModel>>(employees);

            return View(employeeViewModels);
        }

        [HttpGet]
        public IActionResult Registration()
        {
            IEnumerable<Employee> employees = _employeeService.AllActive();

            IEnumerable<EmployeeViewModel> employeeViewModels = _mapper.Map<IEnumerable<EmployeeViewModel>>(employees);

            return View(employeeViewModels);
        }

        [HttpGet]
        public IActionResult View(int id)
        {
            Employee employee = _employeeService.GetById(id);

            EmployeeViewModel employeeViewModel = _mapper.Map<EmployeeViewModel>(employee);

            return View(employeeViewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new CreateEmployeeForm());
        }

        [HttpPost]
        public IActionResult Create(CreateEmployeeForm form)
        {
            return Form(
                form,
                (int employeeId) => this.RedirectToAction(c => c.View(employeeId)),
                () => View(form));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Employee employee = _employeeService.GetById(id);

            EditEmployeeForm editEmployeeForm = _mapper.Map<EditEmployeeForm>(employee);

            return View(editEmployeeForm);
        }

        [HttpPost]
        public IActionResult Edit(EditEmployeeForm form)
        {
            return Form(
                form,
                () => this.RedirectToAction(c => c.View(form.Id)),
                () => View(form));
        }

        [HttpPost]
        public IActionResult Delete(DeleteEmployeeForm form)
        {
            return Form(
                form,
                () => this.RedirectToAction(c => c.List()),
                () => this.RedirectToAction(c => c.View(form.Id)));
        }
    }
}