namespace Web.Application.Controllers.Registration.ViewModels
{
    using System;
    using Domain.Entities.Registration;
    using Employee.ViewModels;



    public class RegistrationViewModel
    {
        public DateTime DateTime { get; set; }

        public RegistrationEventType EventType { get; set; }

        public EmployeeViewModel Employee { get; set; } = new EmployeeViewModel();

        public int Id { get; set; }
        
        public string EntranceCompleteName { get; set; }
    }
}