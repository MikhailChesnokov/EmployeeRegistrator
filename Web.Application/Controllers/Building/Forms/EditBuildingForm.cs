namespace Web.Application.Controllers.Building.Forms
{
    using System.ComponentModel.DataAnnotations;
    
    
    
    public class EditBuildingForm : IForm
    {
        [Required]
        public int? Id { get; set; }

        public string Address { get; set; }
    }
}