namespace Web.Application.Controllers.User.Profiles
{
    using AutoMapper;
    using Domain.Entities.User;
    using ViewModels;



    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserViewModel>()
                .Include<SecurityGuard, SecurityGuardViewModel>();

            CreateMap<SecurityGuard, SecurityGuardViewModel>();
        }
    }
}