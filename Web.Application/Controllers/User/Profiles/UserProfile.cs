namespace Web.Application.Controllers.User.Profiles
{
    using AutoMapper;
    using Domain.Entities.User;
    using Forms;
    using ViewModels;



    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserViewModel>()
                .Include<SecurityGuard, SecurityGuardViewModel>()
                .Include<Manager, ManagerViewModel>()
                .Include<Administrator, UserViewModel>();

            CreateMap<SecurityGuard, SecurityGuardViewModel>();
            CreateMap<Manager, ManagerViewModel>();
            CreateMap<Administrator, UserViewModel>();

            CreateMap<User, EditUserForm>()
                .Include<Manager, EditUserForm>()
                .Include<SecurityGuard, EditUserForm>()
                .Include<Administrator, EditUserForm>();
            
            CreateMap<Manager, EditUserForm>();
            CreateMap<SecurityGuard, EditUserForm>();
            CreateMap<Administrator, EditUserForm>();
        }
    }
}