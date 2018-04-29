namespace Web.Application.Controllers.Account.Forms
{
    using System.ComponentModel.DataAnnotations;



    public class SignUpForm : IForm
    {
        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string ConfirmPassword { get; set; }
    }
}