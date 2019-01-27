namespace Web.Application.Controllers.Building.Profiles
{
    using AutoMapper;
    using Domain.Entities.Building;
    using ViewModels;
    
    
    
    public class BuildingProfile : Profile
    {
        public BuildingProfile()
        {
            CreateMap<Building, BuildingViewModel>();
        }     
    }
}