namespace Web.Application.Controllers.Registration.Forms
{
    using System.ComponentModel.DataAnnotations;



    public class RegisterComingForm : IForm
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int? EmployeeId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int? EntranceId { get; set; }
    }
}