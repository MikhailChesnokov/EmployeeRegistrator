namespace Web.Application.Controllers.Registration.ViewModels
{
    using System.Collections.Generic;
    using Forms;



    public class RegistrationsViewModel
    {
        public IEnumerable<DayRegistrationsViewModel> DayRegistrations { get; set; }

        public ReportFilterForm FilterForm { get; set; }

        public bool IsDocument { get; set; }
    }
}