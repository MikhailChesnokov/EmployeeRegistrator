namespace Web.Application.Controllers.EmployeeRegistration.Forms
{
    public class RegisterEventForm : IForm
    {
        public int EmployeeId { get; set; }

        public string Event { get; set; }
    }
}