namespace Web.Application.Controllers.User.ViewModels
{
    using Domain.Entities.User;



    public class UserViewModel
    {
        public int Id { get; set; }

        public string Login { get; set; }

        public Roles Role { get; set; }

        public string Email { get; set; }

        public bool NeedNotify { get; set; }
    }
}