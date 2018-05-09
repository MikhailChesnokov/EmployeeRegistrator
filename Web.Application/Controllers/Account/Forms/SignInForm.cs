namespace Web.Application.Controllers.Account.Forms
{
    using System.ComponentModel.DataAnnotations;



    public class SignInForm : IForm
    {
        [Required(AllowEmptyStrings = false)]
        public string Login { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Password { get; set; }
    }
}