namespace Web.Application.Controllers.User.Forms
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Domain.Entities.User;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class EditUserForm : IForm
    {
        private const int MaxStringLength = 64;

        [HiddenInput]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [MaxLength(MaxStringLength)]
        [RegularExpression("^.+$")]
        public string Login { get; set; }

        [Required]
        [EnumDataType(typeof(Role))]
        public Role? Role { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public bool NeedNotify { get; set; }

        
        public int? EntranceId { get; set; }
        
        public int? DepartmentId { get; set; }

        public IEnumerable<SelectListItem> Entrances { get; set; }

        public IEnumerable<SelectListItem> Roles { get; set; }
        
        public IEnumerable<SelectListItem> Departments { get; set; }
    }
}