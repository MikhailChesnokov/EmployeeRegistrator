namespace Web.Application.Components.ViewModels
{
    using Domain.Entities.User;



    public class NavbarViewModel
    {
        public bool IsUserAuthorized { get; set; }

        public Role? UserRole { get; set; }
    }
}