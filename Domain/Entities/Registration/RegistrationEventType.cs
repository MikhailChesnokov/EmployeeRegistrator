namespace Domain.Entities.Registration
{
    using System.ComponentModel.DataAnnotations;



    public enum RegistrationEventType
    {
        [Display(Name = "Вход")]
        Coming = 1,

        [Display(Name = "Выход")]
        Leaving = 2
    }
}