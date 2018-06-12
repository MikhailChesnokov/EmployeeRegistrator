namespace Web.Application.Controllers.Registration.ViewModels
{
    using System;
    using System.Collections.Generic;



    public class DayEmployeeRegistraionsViewModel
    {
        public string Employee { get; set; }

        public int EmployeeId { get; set; }

        public int TotalDayEmployeeRegistrationsCount { get; set; }

        public TimeSpan TotalWorkDayTimeInterval { get; set; }

        public TimeSpan LatenessTimeInterval { get; set; }

        public bool EmployeeWasLate { get; set; }

        public IEnumerable<RegistrationRowViewModel> RegistrationRows { get; set; }
    }
}