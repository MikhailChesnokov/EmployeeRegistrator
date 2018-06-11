namespace Web.Application.Controllers.Registration.Abstractions
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc.Rendering;



    public interface ILatenessSelectList
    {
        IEnumerable<SelectListItem> LatenessSelectListItems { get; set; }
    }
}