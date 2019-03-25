namespace Web.Application.Controllers.Entrance.Profiles
{
    using AutoMapper;
    using Domain.Entities.Entrance;
    using Forms;
    using ViewModels;

    
    public class EntranceProfile : Profile
    {
        public EntranceProfile()
        {
            CreateMap<Entrance, EntranceViewModel>();

            CreateMap<Entrance, EditEntranceForm>();
        }
    }
}