namespace Web.Application.Controllers.Entrance.Forms
{
    using System.ComponentModel.DataAnnotations;

    
    
    public class DeleteEntranceForm : IForm
    {
        [Required]
        public int? Id { get; set; }
    }
}