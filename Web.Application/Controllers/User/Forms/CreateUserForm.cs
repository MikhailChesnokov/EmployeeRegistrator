namespace Web.Application.Controllers.User.Forms
{
    using System.ComponentModel.DataAnnotations;
    using Domain.Entities.User;



    public class CreateUserForm : IForm
    {
        private const int MaxStringLength = 64;



        [Required(AllowEmptyStrings = false)]
        [MaxLength(MaxStringLength)]
        [RegularExpression("^.+$")]
        public string Login { get; set; }

        [Required(AllowEmptyStrings = false)]
        [MaxLength(MaxStringLength)]
        [RegularExpression("^.+$")]
        public string Password { get; set; }

        [Required(AllowEmptyStrings = false)]
        [MaxLength(MaxStringLength)]
        [RegularExpression("^.+$")]
        public string ConfirmPassword { get; set; }

        [Required]
        [EnumDataType(typeof(Roles))]
        public Roles Role { get; set; }
    }
}