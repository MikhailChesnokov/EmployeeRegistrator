namespace Web.Application.Controllers.Registration.Enums
{
    using System.ComponentModel.DataAnnotations;



    public enum StrictSchedureRequirement
    {
        [Display(Name = "Да")]
        Yes = 1,

        [Display(Name = "Нет")]
        No = 2
    }
}