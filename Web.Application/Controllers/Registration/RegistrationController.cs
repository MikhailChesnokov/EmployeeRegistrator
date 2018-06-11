namespace Web.Application.Controllers.Registration
{
    using System;
    using System.Collections.Generic;
    using AutoMapper;
    using Domain.Entities.Registration;
    using Domain.Entities.User;
    using Domain.Services.Employee;
    using Domain.Services.Registration;
    using Domain.Services.Time;
    using Employee;
    using Enums;
    using Filters;
    using Forms;
    using Infrastructure.Extensions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using ViewModels;



    [Authorize]
    public class RegistrationController : FormControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;
        private readonly IRegistrationService _registrationService;
        private readonly ITimeService _timeService;



        public RegistrationController(
            IFormHandlerFactory formHandlerFactory,
            IRegistrationService registrationService,
            IMapper mapper,
            IEmployeeService employeeService,
            IAuthorizationService authorizationService,
            ITimeService timeService)
            : base(
                formHandlerFactory,
                authorizationService)
        {
            _registrationService = registrationService;
            _mapper = mapper;
            _employeeService = employeeService;
            _timeService = timeService;
        }



        [HttpPost]
        public void RegisterComing([FromBody] RegisterComingForm form)
        {
            throw new NotImplementedException();

            Form(form, Ok, () => StatusCode(StatusCodes.Status409Conflict));
        }

        [HttpPost]
        public void RegisterLeaving([FromBody] RegisterLeavingForm form)
        {
            throw new NotImplementedException();

            Form(form, Ok, () => StatusCode(StatusCodes.Status409Conflict));
        }

        [HttpGet]
        public IActionResult RegisterComing(int id)
        {
            if (!RoleIs(Roles.SecurityGuard)) return Forbid();


            return Form(
                new RegisterComingForm {EmployeeId = id},
                () => this.RedirectToAction<EmployeeController>(c => c.Registration()),
                () => this.RedirectToAction<EmployeeController>(c => c.Registration()));
        }

        [HttpGet]
        public IActionResult RegisterLeaving(int id)
        {
            if (!RoleIs(Roles.SecurityGuard)) return Forbid();


            return Form(
                new RegisterLeavingForm {EmployeeId = id},
                () => this.RedirectToAction<EmployeeController>(c => c.Registration()),
                () => this.RedirectToAction<EmployeeController>(c => c.Registration()));
        }

        [HttpGet]
        public IActionResult Report()
        {
            if (!RoleIs(Roles.Administrator, Roles.Manager)) return Forbid();


            IEnumerable<Registration> registrations =
                _registrationService
                    .AllInclude(x => x.Employee);

            IEnumerable<RegistrationViewModel> registrationViewModels = _mapper.Map<IEnumerable<RegistrationViewModel>>(registrations);

            ReportFilterForm filterForm = new ReportFilterForm
            {
                Registrations = registrationViewModels,
                Employees = _employeeService.All().ToSelectList(),
                LatenessSelectListItems = typeof(Lateness).ToSelectList(),
                StrictScheduleSelecrListItems = typeof(StrictSchedureRequirement).ToSelectList()
            };

            return View(filterForm);
        }

        [HttpPost]
        public IActionResult Report(ReportFilterForm filterForm)
        {
            if (!RoleIs(Roles.Administrator, Roles.Manager)) return Forbid();


            IEnumerable<Registration> registrations =
                _registrationService
                    .AllInclude(x => x.Employee)
                    .ForEmployee(filterForm.EmployeeId)
                    .ForPeriod(filterForm.DateFrom, filterForm.DateTo)
                    .WithStrictScheduleRestriction(filterForm.StrictSchedule)
                    .WithLateness(_timeService, filterForm.Lateness);

            IEnumerable<RegistrationViewModel> registrationsViewModels = _mapper.Map<IEnumerable<RegistrationViewModel>>(registrations);

            filterForm.Registrations = registrationsViewModels;
            filterForm.Employees = _employeeService.All().ToSelectList();
            filterForm.LatenessSelectListItems = typeof(Lateness).ToSelectList(Enum.GetName(typeof(Lateness), filterForm.Lateness));
            filterForm.StrictScheduleSelecrListItems = typeof(StrictSchedureRequirement).ToSelectList();

            return View(filterForm);
        }
    }
}