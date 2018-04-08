namespace Web.Application.Controllers.Employee.Forms
{
    using System.ComponentModel.DataAnnotations;



    public class EditEmployeeForm : IForm
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string Patronymic { get; set; }
    }
}