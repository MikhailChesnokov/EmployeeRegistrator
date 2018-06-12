namespace Web.Application.Components
{
    using Authorization.UserProviders;
    using Domain.Entities.User;
    using Microsoft.AspNetCore.Mvc;
    using ViewModels;



    public class NavbarViewComponent : ViewComponent
    {
        private readonly IUserProvider<User> _userProvider;



        public NavbarViewComponent(IUserProvider<User> userProvider)
        {
            _userProvider = userProvider;
        }



        public IViewComponentResult Invoke()
        {
            NavbarViewModel navbar = new NavbarViewModel
            {
                IsUserAuthorized = User.Identity.IsAuthenticated,
                UserRole = _userProvider.User?.Role
            };

            return View(navbar);
        }
    }
}