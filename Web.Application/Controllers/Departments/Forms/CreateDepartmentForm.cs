namespace Web.Application.Controllers.Departments.Forms
{
    using System.ComponentModel.DataAnnotations;



    public class CreateDepartmentForm : IForm
    {
        private const int MaxStringLength = 64;



        [Required(AllowEmptyStrings = false)]
        [MaxLength(MaxStringLength)]
        [RegularExpression("^[0-9А-Яа-яёЁ,\\- ]+$")]
        public string Name { get; set; }
    }
}