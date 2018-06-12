namespace Web.Application.Components.ViewModels
{
    using Domain.Entities.User;



    public class NavbarViewModel
    {
        public bool IsUserAuthorized { get; set; }

        public Roles? UserRole { get; set; }
    }
}