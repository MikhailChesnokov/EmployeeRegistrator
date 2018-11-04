namespace Web.Application.Controllers.Registration.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;
    using Forms;



    public class RegistrationsViewModel
    {
        public IEnumerable<DayRegistrationsViewModel> DayRegistrations { get; set; }

        public ReportFilterForm FilterForm { get; set; }

        public bool IsDocument { get; set; }

        public int DayRegistrationsCount => DayRegistrations.Sum(x => x.DayEmployeeRegistrationsCount);
    }
}