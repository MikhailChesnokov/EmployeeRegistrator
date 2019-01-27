namespace Web.Application.Controllers.Entrance.Forms
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Mvc.Rendering;

    
    
    public class CreateEntranceForm : IForm
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int? BuildingId { get; set; }

        public IEnumerable<SelectListItem> Buildings { get; set; }
    }
}