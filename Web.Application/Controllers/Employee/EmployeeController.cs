namespace Web.Application.Controllers.Employee
{
    using System.Collections.Generic;
    using AutoMapper;
    using Domain.Entities;
    using Domain.Repository;
    using Forms;
    using Forms.Handlers;
    using Microsoft.AspNetCore.Mvc;
    using ViewModels;



    public class EmployeeController : Controller
    {
        private readonly IRepository<Employee> _employeeRepository;

        private readonly IMapper _mapper;



        public EmployeeController(
            IRepository<Employee> employeeRepository,
            IMapper mapper)
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
            if (ModelState.IsValid)
                try
                {
                    int employeeId = new CreateEmployeeFormHandler(_employeeRepository).Execute(form);

                    return this.RedirectToAction(c => c.View(employeeId));
                }
                catch (FormException e)
                {
                    ModelState.AddModelError(string.Empty, e.Message);
                }

            return View(form);
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
            if (ModelState.IsValid)
                try
                {
                    new EditEmployeeFormHandler(_employeeRepository).Execute(form);

                    return this.RedirectToAction(c => c.View(form.Id));
                }
                catch (FormException e)
                {
                    ModelState.AddModelError(string.Empty, e.Message);
                }

            return View(form);
        }

        [HttpPost]
        public IActionResult Delete(DeleteEmployeeForm form)
        {
            new DeleteEmployeeFormHandler(_employeeRepository).Execute(form);

            return this.RedirectToAction(c => c.List());
        }
    }
}