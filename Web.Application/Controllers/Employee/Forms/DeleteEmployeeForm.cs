namespace Web.Application.Controllers.Employee.Forms
{
    using System.ComponentModel.DataAnnotations;



    public class DeleteEmployeeForm : IForm
    {
        [Required]
        public int Id { get; set; }
    }
}