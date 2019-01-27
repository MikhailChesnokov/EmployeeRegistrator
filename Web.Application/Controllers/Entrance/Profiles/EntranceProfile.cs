namespace Web.Application.Controllers.Entrance.Profiles
{
    using AutoMapper;
    using Domain.Entities.Entrance;
    using ViewModels;

    
    public class EntranceProfile : Profile
    {
        public EntranceProfile()
        {
            CreateMap<Entrance, EntranceViewModel>();
        }
    }
}