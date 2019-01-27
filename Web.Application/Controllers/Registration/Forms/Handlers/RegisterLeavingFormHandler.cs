namespace Web.Application.Controllers.Registration.Forms.Handlers
{
    using Domain.Entities.Registration;
    using Domain.Services.Employee;
    using Domain.Services.Entrance;
    using Domain.Services.Registration;



    public class RegisterLeavingFormHandler : IFormHandler<RegisterLeavingForm>
    {
        private readonly IRegistrationService _registrationService;
        private readonly IEmployeeService _employeeService;
        private readonly IEntranceService _entranceService;



        public RegisterLeavingFormHandler(
            IRegistrationService registrationService,
            IEmployeeService employeeService,
            IEntranceService entranceService)
        {
            _registrationService = registrationService;
            _employeeService = employeeService;
            _entranceService = entranceService;
        }



        public void Execute(RegisterLeavingForm form)
        {
            if (!form.EmployeeId.HasValue)
                throw new FormException("Employee not chosen.");
            
            var employee = _employeeService.GetById(form.EmployeeId.Value);

            if (employee is null)
                throw new FormException("Employee not found.");

            if (!form.EntranceId.HasValue)
                throw new FormException("Entrance not chosen.");

            var entrance = _entranceService.GetById(form.EntranceId.Value);
            
            _registrationService.RegisterEmployee(employee, RegistrationEventType.Leaving, entrance);
        }
    }
}