namespace Web.Application.Controllers.Registration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using Domain.Entities.Registration;
    using Domain.Entities.User;
    using Domain.Services.Employee;
    using Domain.Services.Registration;
    using Domain.Services.Time;
    using Enums;
    using Filters;
    using Forms;
    using Infrastructure.Extensions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using Services.ExcelGenerator;
    using Services.HtmlLayoutGenerator;
    using Services.PdfGenerator;
    using Services.RegistrationsViewModel;
    using ViewModels;


    [Authorize]
    public sealed class RegistrationController : FormControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;
        private readonly IRegistrationService _registrationService;
        private readonly ITimeService _timeService;
        private readonly IRegistrationsViewModelService _registrationsViewModelService;
        private readonly IRazorHtmlLayoutGenerator _htmlLayoutGenerator;
        private readonly IPdfGenerator _pdfGenerator;
        private readonly IExcelGenerator _excelGenerator;


        public RegistrationController(
            IFormHandlerFactory formHandlerFactory,
            IRegistrationService registrationService,
            IMapper mapper,
            IEmployeeService employeeService,
            IAuthorizationService authorizationService,
            ITimeService timeService,
            IRegistrationsViewModelService registrationsViewModelService,
            IRazorHtmlLayoutGenerator htmlLayoutGenerator,
            IPdfGenerator pdfGenerator,
            IExcelGenerator excelGenerator)
            : base(
                formHandlerFactory,
                authorizationService)
        {
            _registrationService = registrationService;
            _mapper = mapper;
            _employeeService = employeeService;
            _timeService = timeService;
            _registrationsViewModelService = registrationsViewModelService;
            _htmlLayoutGenerator = htmlLayoutGenerator;
            _pdfGenerator = pdfGenerator;
            _excelGenerator = excelGenerator;
        }


        [HttpGet]
        public IActionResult RegisterComing(RegisterComingForm form)
        {
            if (!RoleIs(Role.SecurityGuard))
                return Forbid();

            return Form(form, Ok, BadRequest);
        }

        [HttpGet]
        public IActionResult RegisterLeaving(RegisterLeavingForm form)
        {
            if (!RoleIs(Role.SecurityGuard))
                return Forbid();

            return Form(form, Ok, BadRequest);
        }

        [HttpGet]
        public IActionResult List()
        {
            if (!RoleIs(Role.Administrator, Role.Manager))
                return Forbid();

            var registrations = _registrationService.AllInclude(x => x.Employee, x => x.Entrance.Building);

            return View(GetViewModel(registrations, new ReportFilterForm()));
        }

        [HttpPost]
        public IActionResult List(ReportFilterForm form, string pdf, string excel)
        {
            if (!RoleIs(Role.Administrator, Role.Manager))
            {
                return Forbid();
            }

            var registrations =
                _registrationService
                    .AllInclude(x => x.Employee, x => x.Entrance.Building)
                    .ForEmployee(form.EmployeeId)
                    .ForPeriod(form.DateFrom, form.DateTo)
                    .WithStrictScheduleRestriction(form.StrictSchedule)
                    .WithLateness(_timeService, form.Lateness)
                    .ToList();

            var viewModel = GetViewModel(registrations, form);

            if (string.IsNullOrWhiteSpace(pdf) &&
                string.IsNullOrWhiteSpace(excel))
            {
                return View(viewModel);
            }

            viewModel.IsDocument = true;

            if (pdf != null)
            {
                var htmlContent = _htmlLayoutGenerator.RenderAsync($"Registration/{nameof(List)}", viewModel)
                    .GetAwaiter().GetResult();

                var file = _pdfGenerator.GenerateAsync(htmlContent).GetAwaiter().GetResult();

                return File(file, "application/json",
                    $"Отчет_{DateTime.Now.ToShortDateString()}_{DateTime.Now.ToShortTimeString()}.pdf");
            }
            else
            {
                var file = _excelGenerator.GenerateAsync(viewModel).GetAwaiter().GetResult();

                return File(file, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    $"Отчет_{DateTime.Now.ToShortDateString()}_{DateTime.Now.ToShortTimeString()}.xlsx");
            }
        }

        private RegistrationsViewModel GetViewModel(IEnumerable<Registration> registrations, ReportFilterForm form)
        {
            var registrationViewModels = _mapper.Map<IEnumerable<RegistrationViewModel>>(registrations);

            var selectedLateness = form.Lateness.HasValue
                ? Enum.GetName(typeof(Lateness), form.Lateness)
                : string.Empty;
            var selectedScheduleRestriction = form.StrictSchedule.HasValue
                ? Enum.GetName(typeof(StrictSchedureRequirement), form.StrictSchedule)
                : string.Empty;

            form.Registrations = registrationViewModels;
            form.Employees = _employeeService.All().ToSelectList();
            form.LatenessSelectListItems = typeof(Lateness).ToSelectList(selectedLateness);
            form.StrictScheduleSelecrListItems = typeof(StrictSchedureRequirement).ToSelectList(selectedScheduleRestriction);

            var registrationsViewModel = _registrationsViewModelService.ToRegistrationsViewModel(registrationViewModels, form);

            return registrationsViewModel;
        }


        [HttpGet]
        public IActionResult StackedBar()
        {
            if (!RoleIs(Role.Administrator, Role.Manager))
            {
                return Forbid();
            }

            var registrations =
                _registrationService
                    .AllInclude(x => x.Employee, x => x.Entrance.Building);

            return StackedBar(registrations, new ReportFilterForm());
        }

        [HttpPost]
        public IActionResult StackedBar(ReportFilterForm form)
        {
            if (!RoleIs(Role.Administrator, Role.Manager))
            {
                return Forbid();
            }

            var registrations =
                _registrationService
                    .AllInclude(x => x.Employee, x => x.Entrance.Building)
                    .ForEmployee(form.EmployeeId)
                    .ForPeriod(form.DateFrom, form.DateTo)
                    .WithStrictScheduleRestriction(form.StrictSchedule)
                    .WithLateness(_timeService, form.Lateness);

            return StackedBar(registrations, form);
        }

        private ViewResult StackedBar(IEnumerable<Registration> registrations, ReportFilterForm form)
        {
            IEnumerable<RegistrationViewModel> registrationViewModels =
                _mapper.Map<IEnumerable<RegistrationViewModel>>(registrations);

            string selectedLateness =
                form.Lateness.HasValue ? Enum.GetName(typeof(Lateness), form.Lateness) : string.Empty;
            string selectedScheduleRestriction = form.StrictSchedule.HasValue
                ? Enum.GetName(typeof(StrictSchedureRequirement), form.StrictSchedule)
                : string.Empty;

            form.Registrations = registrationViewModels;
            form.Employees = _employeeService.All().ToSelectList();
            form.LatenessSelectListItems = typeof(Lateness).ToSelectList(selectedLateness);
            form.StrictScheduleSelecrListItems =
                typeof(StrictSchedureRequirement).ToSelectList(selectedScheduleRestriction);

            RegistrationsViewModel registraionsViewModel =
                _registrationsViewModelService.ToRegistrationsViewModel(registrationViewModels, form);

            List<StackedBarDayViewModel> barViewModels =
                registraionsViewModel
                    .DayRegistrations
                    .Select(dayRegistrations =>
                    {
                        IOrderedEnumerable<DayEmployeeRegistraionsViewModel> orderedDayEmployeeRegistrations =
                            dayRegistrations.DayEmployeeRegistraions.OrderBy(x => x.EmployeeId);


                        return new StackedBarDayViewModel
                        {
                            Day = dayRegistrations.Day.DayOfYear.ToString(),
                            Names =
                                JsonConvert.SerializeObject(orderedDayEmployeeRegistrations.Select(x => x.Employee)),
                            WorkTimes = JsonConvert.SerializeObject(orderedDayEmployeeRegistrations.Select(x =>
                            {
                                int totalMinutes = (int) x.TotalWorkDayTimeInterval.TotalMinutes;

                                if (dayRegistrations.Day.Date.Equals(_timeService.Now.Date))
                                {
                                    RegistrationRowViewModel last = x.RegistrationRows.OrderBy(y => y.Time).Last();

                                    if (last.Event.Equals(RegistrationEventType.Coming))
                                    {
                                        totalMinutes += (int) (_timeService.TimeNow - last.Time).TotalMinutes;
                                    }
                                }

                                return totalMinutes.ToString();
                            })),
                            LatenessTimes = JsonConvert.SerializeObject(orderedDayEmployeeRegistrations.Select(x =>
                            {
                                int totalMinutes = (int) x.LatenessTimeInterval.TotalMinutes;

                                if (dayRegistrations.Day.Date.Equals(_timeService.Now.Date))
                                {
                                    RegistrationRowViewModel last = x.RegistrationRows.OrderBy(y => y.Time).Last();

                                    if (last.Event.Equals(RegistrationEventType.Coming))
                                    {
                                        if (totalMinutes < 1 && last.Time >= _timeService.WorkDayStartsAt)
                                        {
                                            totalMinutes = (int) (last.Time - _timeService.WorkDayStartsAt)
                                                .TotalMinutes;
                                        }
                                    }
                                }
                                else if (x.RegistrationRows.All(z =>
                                    z.CheckResult == RegistrationCheckResult.Violation))
                                {
                                    totalMinutes = (int) _timeService.TotalWorkDayTimeSpan.TotalMinutes;
                                }

                                return totalMinutes.ToString();
                            }))
                        };
                    })
                    .ToList();

            StackedBarViewModel viewModel = new StackedBarViewModel
            {
                StackedBarDayViewModels = barViewModels,
                FilterForm = form,
                DayRegistrations = registraionsViewModel.DayRegistrations
            };

            return View(viewModel);
        }
    }
}