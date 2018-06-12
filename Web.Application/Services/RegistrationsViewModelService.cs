namespace Web.Application.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Controllers.Registration.Enums;
    using Controllers.Registration.Forms;
    using Controllers.Registration.ViewModels;
    using Domain.Entities.Registration;
    using Domain.Services.Time;



    public class RegistrationsViewModelService : IRegistrationsViewModelService
    {
        private readonly ITimeService _timeService;


        public RegistrationsViewModelService(ITimeService timeService)
        {
            _timeService = timeService;
        }



        public RegistrationsViewModel ToRegistrationsViewModel(
            IEnumerable<RegistrationViewModel> registrations,
            ReportFilterForm filterForm)
        {
            return new RegistrationsViewModel
            {
                DayRegistrations = ToDayRegistrations(registrations),
                FilterForm = filterForm
            };
        }

        private IEnumerable<DayRegistrationsViewModel> ToDayRegistrations(IEnumerable<RegistrationViewModel> registrations)
        {
            return
                registrations
                    .GroupBy(x => x.DateTime.DayOfYear)
                    .Select(dayRegistrations => new DayRegistrationsViewModel
                    {
                        Day = dayRegistrations.First().DateTime.Date,
                        TotalDayRegistrationsCount = dayRegistrations.Count(),
                        DayEmployeeRegistraions = ToDayEmployeeRegistrations(dayRegistrations)
                    });
        }

        private IEnumerable<DayEmployeeRegistraionsViewModel> ToDayEmployeeRegistrations(IEnumerable<RegistrationViewModel> dayRegistrations)
        {
            return
                dayRegistrations
                    .GroupBy(x => x.Employee.Id)
                    .Select(dayEmployeeRegistrations =>
                    {
                        CheckedRegistrationViewModel[] checkedDayEmployeeRegistrations = ToCheckedDayEmployeeRegistrations(dayEmployeeRegistrations);

                        return new DayEmployeeRegistraionsViewModel
                        {
                            Employee = dayEmployeeRegistrations.First().Employee.Fio,
                            EmployeeId = dayEmployeeRegistrations.First().Employee.Id,
                            TotalDayEmployeeRegistrationsCount = dayEmployeeRegistrations.Count(),
                            TotalWorkDayTimeInterval = GetTotalWorkDayTimeInterval(checkedDayEmployeeRegistrations),
                            LatenessTimeInterval = GetLatenessTimeInterval(checkedDayEmployeeRegistrations, _timeService),
                            EmployeeWasLate = WasEmployeeLate(checkedDayEmployeeRegistrations, _timeService),
                            RegistrationRows = ToDayEmployeeRegistrationRows(checkedDayEmployeeRegistrations)
                        };
                    });
        }

        private IEnumerable<RegistrationRowViewModel> ToDayEmployeeRegistrationRows(IEnumerable<CheckedRegistrationViewModel> checkedDayEmployeeRegistrations)
        {
            return
                checkedDayEmployeeRegistrations
                    .Select(dayEmployeeRegistration => new RegistrationRowViewModel
                    {
                        Event = dayEmployeeRegistration.Registration.EventType,
                        Time = dayEmployeeRegistration.Registration.DateTime.TimeOfDay,
                        CheckResult = dayEmployeeRegistration.CheckResult,
                        WorkTimeInterval = dayEmployeeRegistration.WorkPeriodTime
                    });
        }

        private CheckedRegistrationViewModel[] ToCheckedDayEmployeeRegistrations(IEnumerable<RegistrationViewModel> dayEmployeeRegistrations)
        {
            RegistrationViewModel[] registrations = dayEmployeeRegistrations.OrderBy(x => x.DateTime).ToArray();

            CheckedRegistrationViewModel[] checkedRegistrations = registrations.Select(x => new CheckedRegistrationViewModel { Registration = x }).ToArray();

            for (int i = 0; i < registrations.Length; i++)
            {
                switch (registrations[i].EventType)
                {
                    case RegistrationEventType.Coming when i + 1 < registrations.Length && registrations[i + 1].EventType is RegistrationEventType.Leaving:
                    case RegistrationEventType.Leaving when i - 1 >= 0 && registrations[i - 1].EventType is RegistrationEventType.Coming:
                        checkedRegistrations[i].CheckResult = RegistrationCheckResult.Ok;
                        break;

                    default:
                        checkedRegistrations[i].CheckResult = RegistrationCheckResult.Violation;
                        break;
                }
            }

            return checkedRegistrations;
        }

        private TimeSpan GetTotalWorkDayTimeInterval(IEnumerable<CheckedRegistrationViewModel> checkedDayEmployeeRegistrations)
        {
            CheckedRegistrationViewModel[] registrations =
                checkedDayEmployeeRegistrations
                    .OrderBy(x => x.Registration.DateTime)
                    .Where(x => x.CheckResult is RegistrationCheckResult.Ok)
                    .ToArray();

            if (registrations.Length is 0)
                return default;

            TimeSpan total = TimeSpan.Zero;

            for (int i = 0; i < registrations.Length; i += 2)
            {
                TimeSpan workPeriodTime =
                    registrations[i + 1].Registration.DateTime -
                    registrations[i].Registration.DateTime;

                registrations[i].WorkPeriodTime = registrations[i].WorkPeriodTime = workPeriodTime;

                total += workPeriodTime;
            }

            return total;
        }

        private TimeSpan GetLatenessTimeInterval(IEnumerable<CheckedRegistrationViewModel> checkedDayEmployeeRegistration, ITimeService timeService)
        {
            CheckedRegistrationViewModel firstDayEmployeeRegistration =
                checkedDayEmployeeRegistration
                    .OrderBy(x => x.Registration.DateTime)
                    .FirstOrDefault(x => x.CheckResult == RegistrationCheckResult.Ok);

            if (firstDayEmployeeRegistration is null)
                return default;

            if (firstDayEmployeeRegistration.Registration.DateTime.TimeOfDay < timeService.WorkDayStartsAt)
                return TimeSpan.Zero;

            return firstDayEmployeeRegistration.Registration.DateTime.TimeOfDay - timeService.WorkDayStartsAt;
        }

        private bool WasEmployeeLate(IEnumerable<CheckedRegistrationViewModel> checkedDayEmployeeRegistration, ITimeService timeService)
        {
            CheckedRegistrationViewModel firstDayEmployeeRegistration =
                checkedDayEmployeeRegistration
                    .OrderBy(x => x.Registration.DateTime)
                    .FirstOrDefault(x => x.CheckResult == RegistrationCheckResult.Ok);

            if (firstDayEmployeeRegistration is null)
                return true;

            return firstDayEmployeeRegistration.Registration.DateTime.TimeOfDay >= timeService.WorkDayStartsAt;
        }
    }
}