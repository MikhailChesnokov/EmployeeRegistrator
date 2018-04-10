namespace Web.Application.Controllers.EmployeeRegistration.Forms
{
    using System;
    using Domain.Entities;
    using Domain.Repository;
    using Domain.Services;



    public class RegisterEventFormHandler : IFormHandler<RegisterEventForm>
    {
        private readonly IRegistrationService _registrationService;

        private readonly IRepository<Employee> _employeeRepository;



        public RegisterEventFormHandler(
            IRegistrationService registrationService,
            IRepository<Employee> employeeRepository)
        {
            _registrationService = registrationService;
            _employeeRepository = employeeRepository;
        }



        public void Execute(RegisterEventForm form)
        {
            switch (form.Event)
            {
                case "Coming": _registrationService.RegisterEmployeeComing(form.EmployeeId);
                    break;

                case "Leaving": _registrationService.RegisterEmployeeLeaving(form.EmployeeId);
                    break;

                default:
                    throw new ArgumentException("Unrecognized event type.");
            }
        }
    }
}