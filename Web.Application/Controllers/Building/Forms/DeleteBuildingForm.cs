namespace Web.Application.Controllers.Building.Forms
{
    using System.ComponentModel.DataAnnotations;

    
    
    public class DeleteBuildingForm : IForm
    {
        [Required]
        public int? Id { get; set; }
    }
}