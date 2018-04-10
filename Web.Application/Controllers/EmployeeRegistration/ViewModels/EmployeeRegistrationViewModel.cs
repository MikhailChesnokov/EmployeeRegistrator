namespace Web.Application.Controllers.EmployeeRegistration.ViewModels
{
    using System;
    using Domain.Entities;



    public class EmployeeRegistrationViewModel
    {
        public DateTime DateTime { get; set; }

        public RegistrationEventType EventType { get; set; }

        public int EmployeeId { get; set; }

        public int Id { get; set; }
    }
}
