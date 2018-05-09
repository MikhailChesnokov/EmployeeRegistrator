namespace Web.Application.Controllers.Account.Forms
{
    using System.ComponentModel.DataAnnotations;



    public class SignUpForm : IForm
    {
        [Required(AllowEmptyStrings = false)]
        public string Login { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Password { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string ConfirmPassword { get; set; }
    }
}