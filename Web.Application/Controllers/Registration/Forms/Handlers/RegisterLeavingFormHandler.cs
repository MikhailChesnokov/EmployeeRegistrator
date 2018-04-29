namespace Web.Application.Controllers.Registration.Forms.Handlers
{
    using Domain.Services.Registration;



    public class RegisterLeavingFormHandler : IFormHandler<RegisterLeavingForm>
    {
        private readonly IRegistrationService _registrationService;



        public RegisterLeavingFormHandler(
            IRegistrationService registrationService)
        {
            _registrationService = registrationService;
        }



        public void Execute(RegisterLeavingForm form)
        {
            _registrationService.RegisterEmployeeLeaving(form.EmployeeId);
        }
    }
}