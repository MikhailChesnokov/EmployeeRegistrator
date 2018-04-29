namespace Web.Application.Controllers.Registration
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using AutoMapper;
    using Domain.Entities.Employee;
    using Domain.Entities.Registration;
    using Domain.Services.Employee;
    using Domain.Services.Registration;
    using Employee;
    using Forms;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using ViewModels;



    [Authorize]
    public class RegistrationController : FormControllerBase
    {
        private readonly IEmployeeService _employeeService;

        private readonly IMapper _mapper;

        private readonly IRegistrationService _registrationService;



        public RegistrationController(
            IFormHandlerFactory formHandlerFactory,
            IRegistrationService registrationService,
            IMapper mapper,
            IEmployeeService employeeService) : base(formHandlerFactory)
        {
            _registrationService = registrationService;
            _mapper = mapper;
            _employeeService = employeeService;
        }



        [HttpPost]
        public void RegisterComing([FromBody] RegisterComingForm form)
        {
            Form(form, Ok, () => StatusCode(StatusCodes.Status409Conflict));
        }

        [HttpPost]
        public void RegisterLeaving([FromBody] RegisterLeavingForm form)
        {
            Form(form, Ok, () => StatusCode(StatusCodes.Status409Conflict));
        }

        [HttpGet]
        public IActionResult RegisterComing(int id)
        {
            return Form(
                new RegisterComingForm {EmployeeId = id},
                () => this.RedirectToAction<EmployeeController>(c => c.Registration()),
                () => this.RedirectToAction<EmployeeController>(c => c.Registration()));
        }

        [HttpGet]
        public IActionResult RegisterLeaving(int id)
        {
            return Form(
                new RegisterLeavingForm {EmployeeId = id},
                () => this.RedirectToAction<EmployeeController>(c => c.Registration()),
                () => this.RedirectToAction<EmployeeController>(c => c.Registration()));
        }

        [HttpGet]
        public IActionResult Report()
        {
            IEnumerable<Registration> registrations = _registrationService.All();

            IEnumerable<RegistrationViewModel> registrationViewModels = _mapper.Map<IEnumerable<RegistrationViewModel>>(registrations);

            IEnumerable<Employee> employees = _employeeService.All();

            ReportForm form = new ReportForm
            {
                Registrations = registrationViewModels,
                Employees = employees.Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Fio
                })
            };

            return View(form);
        }

        [HttpPost]
        public IActionResult Report(ReportForm form)
        {
            IEnumerable<Registration> registrations = _registrationService.All();

            if (form.EmployeeId != null)
                registrations = registrations.Where(x => x.Employee.Id == form.EmployeeId);

            if (form.DateFrom != null)
                registrations = registrations.Where(x => x.DateTime >= form.DateFrom);

            if (form.DateTo != null)
                registrations = registrations.Where(x => x.DateTime < form.DateTo.Value.AddDays(1));

            IEnumerable<RegistrationViewModel> registrationsViewModels = _mapper.Map<IEnumerable<RegistrationViewModel>>(registrations);

            IEnumerable<Employee> employees = _employeeService.All();

            form.Registrations = registrationsViewModels;
            form.Employees = employees.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Fio
            });

            return View(form);
        }
    }
}