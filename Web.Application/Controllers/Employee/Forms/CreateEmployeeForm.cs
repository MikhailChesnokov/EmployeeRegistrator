namespace Web.Application.Controllers.Employee.Forms
{
    using System.ComponentModel.DataAnnotations;



    public class CreateEmployeeForm : IForm
    {
        private const int MaxStringLength = 64;



        [Required(AllowEmptyStrings = false)]
        [MaxLength(MaxStringLength)]
        [RegularExpression("^[А-Яа-яёЁ\\-]+$")]
        public string FirstName { get; set; }

        [Required(AllowEmptyStrings = false)]
        [MaxLength(MaxStringLength)]
        [RegularExpression("^[А-Яа-яёЁ\\-]+$")]
        public string Surname { get; set; }

        [Required(AllowEmptyStrings = false)]
        [MaxLength(MaxStringLength)]
        [RegularExpression("^[А-Яа-яёЁ]+$")]
        public string Patronymic { get; set; }

        [Required(AllowEmptyStrings = false)]
        [MaxLength(MaxStringLength)]
        [RegularExpression("^.+$")]
        public string PersonnelNumber { get; set; }

        public bool WorkplacePresenceRequired { get; set; }
    }
}