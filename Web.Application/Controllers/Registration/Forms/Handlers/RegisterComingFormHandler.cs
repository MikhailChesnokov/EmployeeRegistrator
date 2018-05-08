namespace Web.Application.Controllers.Registration.Forms.Handlers
{
    using Domain.Entities.Employee;
    using Domain.Services.Employee;
    using Domain.Services.Registration;



    public class RegisterComingFormHandler : IFormHandler<RegisterComingForm>
    {
        private readonly IRegistrationService _registrationService;
        private readonly IEmployeeService _employeeService;



        public RegisterComingFormHandler(
            IRegistrationService registrationService,
            IEmployeeService employeeService)
        {
            _registrationService = registrationService;
            _employeeService = employeeService;
        }



        public void Execute(RegisterComingForm form)
        {
            Employee employee = _employeeService.GetById(form.EmployeeId);

            if (employee is null)
                throw new FormException("Employee not found.");

            _registrationService.RegisterEmployeeComing(employee);
        }
    }
}