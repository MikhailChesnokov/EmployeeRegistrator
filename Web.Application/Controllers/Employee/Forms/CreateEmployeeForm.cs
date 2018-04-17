namespace Web.Application.Controllers.Employee.Forms
{
    using System.ComponentModel.DataAnnotations;



    public class CreateEmployeeForm : IForm
    {
        [Required]
        [RegularExpression("^[А-Яа-яёЁ]+$")]
        public string FirstName { get; set; }

        [Required]
        [RegularExpression("^[А-Яа-яёЁ\\-]+$")]
        public string Surname { get; set; }

        [Required]
        [RegularExpression("^[А-Яа-яёЁ]+$")]
        public string Patronymic { get; set; }

        [Required(AllowEmptyStrings = false)]
        [RegularExpression("^.+$")]
        public string PersonnelNumber { get; set; }

        public bool WorkplacePresenceRequired { get; set; }
    }
}