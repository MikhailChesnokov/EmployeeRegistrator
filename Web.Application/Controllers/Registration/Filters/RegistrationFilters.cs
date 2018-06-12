namespace Web.Application.Controllers.Registration.Filters
{
    using System;
    using System.Linq;
    using Domain.Entities.Registration;
    using Domain.Services.Time;
    using Enums;
    using Infrastructure.Extensions;



    public static class RegistrationFilters
    {
        public static IQueryable<Registration> ForPeriod(this IQueryable<Registration> registrations,
            DateTime? from = null,
            DateTime? to = null)
        {
            if (from.HasValue)
                registrations = registrations
                    .Where(x => x.DateTime >= from.Value.Date);


            if (to.HasValue)
                registrations = registrations
                    .Where(x => x.DateTime <= to.Value.Date.AddDays(1));

            return registrations;
        }

        public static IQueryable<Registration> ForEmployee(this IQueryable<Registration> registrations,
            int? employeeId)
        {
            if (employeeId.HasValue)
                registrations = registrations
                    .Where(x => x.Employee.Id == employeeId);

            return registrations;
        }

        public static IQueryable<Registration> WithLateness(this IQueryable<Registration> registrations,
            ITimeService timeService,
            Lateness? lateness)
        {
            if (lateness.HasValue)
            {
                foreach (var dayRegistrations in registrations.GroupBy(x => x.DateTime.DayOfYear))
                foreach (var dayEmployeeRegistrations in dayRegistrations.GroupBy(x => x.Employee.Id))
                {
                    Registration firstRegistration = dayEmployeeRegistrations.OrderBy(x => x.DateTime).First();

                    TimeSpan employeeLateness = firstRegistration.DateTime.TimeOfDay - timeService.WorkDayStartsAt;

                    switch (lateness)
                    {
                        case Lateness.No when employeeLateness.Ticks > 0:
                        case Lateness.LessThanTimeSpan when employeeLateness > lateness.Value.GetLatenessTimeSpan():
                        case Lateness.MoreThanTimeSpan when employeeLateness < lateness.Value.GetLatenessTimeSpan():

                            registrations = registrations.Except(dayEmployeeRegistrations);
                            break;
                    }
                }
            }

            return registrations;
        }

        public static IQueryable<Registration> WithStrictScheduleRestriction(this IQueryable<Registration> registrations,
            StrictSchedureRequirement? strictSchedure)
        {
            if (strictSchedure.HasValue)
                registrations =
                    strictSchedure == StrictSchedureRequirement.Yes ?
                    registrations.Where(x => x.Employee.WorkplacePresenceRequired) :
                    registrations.Where(x =>!x.Employee.WorkplacePresenceRequired);

            return registrations;
        }
    }
}