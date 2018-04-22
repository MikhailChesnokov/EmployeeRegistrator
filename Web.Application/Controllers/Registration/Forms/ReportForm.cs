namespace Web.Application.Controllers.Registration.Forms
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using ViewModels;



    public class ReportForm : IForm
    {
        [DataType(DataType.Date)]
        public DateTime? DateFrom { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateTo { get; set; }

        public int? EmployeeId { get; set; }

        public IEnumerable<RegistrationViewModel> Registrations { get; set; }

        public IEnumerable<SelectListItem> Employees { get; set; }
    }
}