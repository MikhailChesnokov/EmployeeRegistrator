namespace Web.Application.Controllers.Registration.ViewModels
{
    using System.Collections.Generic;
    using Forms;



    public class StackedBarViewModel
    {
        public IEnumerable<StackedBarDayViewModel> StackedBarDayViewModels { get; set; }

        public ReportFilterForm FilterForm { get; set; }

        public IEnumerable<DayRegistrationsViewModel> DayRegistrations { get; set; }
    }
}