namespace Web.Application.Controllers.Employee.Forms
{
    using System.ComponentModel.DataAnnotations;



    public class CreateEmployeeForm
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string Patronymic { get; set; }
    }
}