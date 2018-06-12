namespace Web.Application.Controllers.Registration.ViewModels
{
    using System;
    using System.Collections.Generic;



    public class DayRegistrationsViewModel
    {
        public int TotalDayRegistrationsCount { get; set; }

        public DateTime Day { get; set; }

        public IEnumerable<DayEmployeeRegistraionsViewModel> DayEmployeeRegistraions { get; set; }
    }
}