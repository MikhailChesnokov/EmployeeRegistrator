namespace Web.Application.Components
{
    using Microsoft.AspNetCore.Mvc;
    using ViewModels;



    public class NavbarViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            NavbarViewModel navbar = new NavbarViewModel
            {
                IsUserAuthorized = User.Identity.IsAuthenticated
            };

            return View(navbar);
        }
    }
}