namespace Web.Application.Controllers.Building.Forms
{
    using System.ComponentModel.DataAnnotations;
    
    
    
    public class CreateBuildingForm : IForm
    {
        [Required]
        public string Address { get; set; }
    }
}