namespace Web.Application.Controllers.Registration.Enums
{
    using System.ComponentModel.DataAnnotations;
    



    public enum Lateness
    {
        [Display(Name = "Без опозданий")]
        [LatenessTime(hours: 0, minutes: 0)]
        No = 1,

        [Display(Name = "Опоздание менее чем на 15 минут")]
        [LatenessTime(hours: 0, minutes: 15)]
        LessThanTimeSpan = 2,

        [Display(Name = "Опоздание более чем на 15 минут")]
        [LatenessTime(hours: 0, minutes: 15)]
        MoreThanTimeSpan = 3
    }
}