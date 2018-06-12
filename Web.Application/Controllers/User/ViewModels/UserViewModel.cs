namespace Web.Application.Controllers.User.ViewModels
{
    using Domain.Entities.User;



    public class UserViewModel
    {
        public string Login { get; set; }

        public Roles Role { get; set; }
    }
}