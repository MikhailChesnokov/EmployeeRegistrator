namespace Web.Application.Controllers.Registration.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;



    public class DayRegistrationsViewModel
    {
        public int TotalDayRegistrationsCount { get; set; }

        public DateTime Day { get; set; }

        public IEnumerable<DayEmployeeRegistraionsViewModel> DayEmployeeRegistraions { get; set; }

        public int DayEmployeeRegistrationsCount => DayEmployeeRegistraions.Sum(x => x.TotalDayEmployeeRegistrationsCount);
    }
}