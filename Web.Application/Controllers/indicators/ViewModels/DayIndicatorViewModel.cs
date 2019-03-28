namespace Web.Application.Controllers.indicators.ViewModels
{
    using System;
    using System.Collections.Generic;
    using Employee.ViewModels;

    public class DayIndicatorViewModel
    {
        public DateTime DateTime { get; set; }

        public int TotalCameCount { get; set; }

        public int TotalGoneCount { get; set; }

        public int TotalInCount => TotalCameCount - TotalGoneCount;

        public IEnumerable<EmployeeViewModel> In { get; set; }
    }
}