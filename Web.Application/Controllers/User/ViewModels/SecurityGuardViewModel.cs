namespace Web.Application.Controllers.User.ViewModels
{
    using Entrance.ViewModels;

    public class SecurityGuardViewModel : UserViewModel
    {
        public EntranceViewModel Entrance { get; set; }
    }
}