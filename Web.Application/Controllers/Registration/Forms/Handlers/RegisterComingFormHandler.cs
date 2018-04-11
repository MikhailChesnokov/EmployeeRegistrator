namespace Web.Application.Controllers.Registration.Forms.Handlers
{
    using Domain.Services.Registration;
    using Forms;



    public class RegisterComingFormHandler : IFormHandler<RegisterComingForm>
    {
        private readonly IRegistrationService _registrationService;



        public RegisterComingFormHandler(
            IRegistrationService registrationService)
        {
            _registrationService = registrationService;
        }



        public void Execute(RegisterComingForm form)
        {
            _registrationService.RegisterEmployeeComing(form.EmployeeId);
        }
    }
}