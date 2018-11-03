namespace Web.Application.Controllers.Departments.Forms
{
    using System.ComponentModel.DataAnnotations;



    public class DeleteDepartmentForm : IForm
    {
        [Required]
        public int? Id { get; set; }
    }
}