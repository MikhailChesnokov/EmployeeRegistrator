namespace Web.Application.Controllers.Registration.Forms
{
    using System.ComponentModel.DataAnnotations;



    public class RegisterLeavingForm : IForm
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int EmployeeId { get; set; }
    }
}