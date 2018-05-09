namespace Web.Application.Controllers.Registration
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using Domain.Entities.Employee;
    using Domain.Entities.Registration;
    using Domain.Services.Employee;
    using Domain.Services.Registration;
    using Employee;
    using Enums;
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
                }),
                LatenessCases = new[]
                {
                    new SelectListItem
                    {
                        Value = ((int)Lateness.No).ToString(),
                        Text = "Без опозданий"
                    },
                    new SelectListItem
                    {
                        Value = ((int)Lateness.LessThanFiftyMinutes).ToString(),
                        Text = "Опоздание не более чем на 15 минут"
                    },
                    new SelectListItem
                    {
                        Value = ((int)Lateness.MoreThanFiftyMinutes).ToString(),
                        Text = "Опоздание более чем на 15 минут"
                    }
                },
                StrictScheduleCases = new []
                {
                    new SelectListItem
                    {
                        Value = ((int)StrictSchedureRequirement.Yes).ToString(),
                        Text = "Строгое соблюдение рабочего графика обязательно"
                    },
                    new SelectListItem
                    {
                        Value = ((int)StrictSchedureRequirement.No).ToString(),
                        Text = "Строгое соблюдение рабочего графика не обязательно"
                    }
                }
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

            List<Registration> registrationsResult = new List<Registration>();

            if (form.Lateness != null)
            {
                switch (form.Lateness)
                {
                    case (int)Lateness.No:
                        foreach (IGrouping<int, Registration> grouping1 in registrations.OrderBy(x => x.DateTime).GroupBy(x => x.DateTime.DayOfYear))
                        {
                            foreach (IGrouping<int, Registration> grouping2 in grouping1.OrderBy(x => x.DateTime).GroupBy(x => x.Employee.Id))
                            {
                                Registration first = grouping2.OrderBy(x => x.DateTime).First();

                                if (first.DateTime.Hour < 10 || first.DateTime.Hour == 10 && first.DateTime.Minute == 0)
                                {
                                    registrationsResult.AddRange(grouping2);
                                }
                            }
                        }
                        break;

                    case (int)Lateness.LessThanFiftyMinutes:
                        foreach (IGrouping<int, Registration> grouping1 in registrations.OrderBy(x => x.DateTime).GroupBy(x => x.DateTime.DayOfYear))
                        {
                            foreach (IGrouping<int, Registration> grouping2 in grouping1.OrderBy(x => x.DateTime).GroupBy(x => x.Employee.Id))
                            {
                                Registration first = grouping2.OrderBy(x => x.DateTime).First();

                                if (first.DateTime.Hour == 10 && first.DateTime.Minute <= 15)
                                {
                                    registrationsResult.AddRange(grouping2);
                                }
                            }
                        }
                        break;

                    case (int)Lateness.MoreThanFiftyMinutes:
                        foreach (IGrouping<int, Registration> grouping1 in registrations.OrderBy(x => x.DateTime).GroupBy(x => x.DateTime.DayOfYear))
                        {
                            foreach (IGrouping<int, Registration> grouping2 in grouping1.OrderBy(x => x.DateTime).GroupBy(x => x.Employee.Id))
                            {
                                Registration first = grouping2.OrderBy(x => x.DateTime).First();

                                if (first.DateTime.Hour == 10 && first.DateTime.Minute > 15 || first.DateTime.Hour > 10)
                                {
                                    registrationsResult.AddRange(grouping2);
                                }
                            }
                        }
                        break;
                }
            }
            else
            {
                registrationsResult.AddRange(registrations);
            }

            IEnumerable<RegistrationViewModel> registrationsViewModels = _mapper.Map<IEnumerable<RegistrationViewModel>>(registrationsResult);

            if (form.StrictSchedule != null)
            {
                switch (form.StrictSchedule)
                {
                    case (int)StrictSchedureRequirement.Yes:
                        registrationsViewModels = registrationsViewModels.Where(x => x.Employee.WorkplacePresenceRequired);
                        break;

                    case (int)StrictSchedureRequirement.No:
                        registrationsViewModels = registrationsViewModels.Where(x => !x.Employee.WorkplacePresenceRequired);
                        break;
                }
            }

            IEnumerable<Employee> employees = _employeeService.All();

            form.Registrations = registrationsViewModels;
            form.Employees = employees.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Fio
            });
            form.LatenessCases = new[]
            {
                new SelectListItem
                {
                    Value = ((int)Lateness.No).ToString(),
                    Text = "Без опозданий"
                },
                new SelectListItem
                {
                    Value = ((int)Lateness.LessThanFiftyMinutes).ToString(),
                    Text = "Опоздание не более чем на 15 минут"
                },
                new SelectListItem
                {
                    Value = ((int)Lateness.MoreThanFiftyMinutes).ToString(),
                    Text = "Опоздание более чем на 15 минут"
                }
            };
            form.StrictScheduleCases = new[]
            {
                new SelectListItem
                {
                    Value = ((int)StrictSchedureRequirement.Yes).ToString(),
                    Text = "Строгое соблюдение рабочего графика обязательно"
                },
                new SelectListItem
                {
                    Value = ((int)StrictSchedureRequirement.No).ToString(),
                    Text = "Строгое соблюдение рабочего графика не обязательно"
                }
            };

            return View(form);
        }
    }
}