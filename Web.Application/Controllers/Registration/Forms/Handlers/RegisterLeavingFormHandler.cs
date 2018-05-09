namespace Web.Application.Controllers.Registration.Forms.Handlers
{
    using Domain.Entities.Employee;
    using Domain.Entities.Registration;
    using Domain.Services.Employee;
    using Domain.Services.Registration;



    public class RegisterLeavingFormHandler : IFormHandler<RegisterLeavingForm>
    {
        private readonly IRegistrationService _registrationService;
        private readonly IEmployeeService _employeeService;



        public RegisterLeavingFormHandler(
            IRegistrationService registrationService,
            IEmployeeService employeeService)
        {
            _registrationService = registrationService;
            _employeeService = employeeService;
        }



        public void Execute(RegisterLeavingForm form)
        {
            Employee employee = _employeeService.GetById(form.EmployeeId);

            if (employee is null)
                throw new FormException("Employee not found.");

            _registrationService.RegisterEmployee(employee, RegistrationEventType.Leaving);
        }
    }
}