namespace Domain.Entities.User
{
    using System.ComponentModel.DataAnnotations;



    public enum Roles
    {
        [Display(Name = "Администратор")]
        Administrator = 1,

        [Display(Name = "Менеджер")]
        Manager = 2,

        [Display(Name = "Охранник")]
        SecurityGuard = 3
    }
}