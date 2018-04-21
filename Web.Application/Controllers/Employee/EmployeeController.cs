namespace Web.Application.Controllers.Employee
{
    using System.Collections.Generic;
    using AutoMapper;
    using Domain.Entities.Employee;
    using Domain.Repository;
    using Forms;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using ViewModels;



    [Authorize]
    public class EmployeeController : FormControllerBase
    {
        private readonly IRepository<Employee> _employeeRepository;

        private readonly IMapper _mapper;



        public EmployeeController(
            IFormHandlerFactory formHandlerFactory,
            IRepository<Employee> employeeRepository,
            IMapper mapper)
            : base(formHandlerFactory)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }



        [HttpGet]
        public IActionResult List()
        {
            IEnumerable<Employee> employees = _employeeRepository.All();

            IEnumerable<EmployeeViewModel> employeeViewModels = _mapper.Map<IEnumerable<EmployeeViewModel>>(employees);

            return View(employeeViewModels);
        }

        [HttpGet]
        public IActionResult Registration()
        {
            IEnumerable<Employee> employees = _employeeRepository.All();

            IEnumerable<EmployeeViewModel> employeeViewModels = _mapper.Map<IEnumerable<EmployeeViewModel>>(employees);

            return View(employeeViewModels);
        }

        [HttpGet]
        public IActionResult View(int id)
        {
            Employee employee = _employeeRepository.FindById(id);

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
            Employee employee = _employeeRepository.FindById(id);

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