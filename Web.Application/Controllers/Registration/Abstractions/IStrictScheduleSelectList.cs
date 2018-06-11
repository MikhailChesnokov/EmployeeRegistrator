namespace Web.Application.Controllers.Registration.Abstractions
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc.Rendering;



    public interface IStrictScheduleSelectList
    {
        IEnumerable<SelectListItem> StrictScheduleSelecrListItems { get; set; }
    }
}