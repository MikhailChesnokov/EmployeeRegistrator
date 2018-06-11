namespace Web.Application.Controllers.Registration.Forms
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Abstractions;
    using Enums;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using ViewModels;



    public class ReportFilterForm :
        IForm,
        ILatenessSelectList,
        IStrictScheduleSelectList
    {
        [DataType(DataType.Date)]
        public DateTime? DateFrom { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateTo { get; set; }

        public int? EmployeeId { get; set; }

        public Lateness? Lateness { get; set; }

        public StrictSchedureRequirement? StrictSchedule { get; set; }

        public IEnumerable<RegistrationViewModel> Registrations { get; set; }

        public IEnumerable<SelectListItem> Employees { get; set; }

        public IEnumerable<SelectListItem> LatenessSelectListItems { get; set; }

        public IEnumerable<SelectListItem> StrictScheduleSelecrListItems { get; set; }
    }
}