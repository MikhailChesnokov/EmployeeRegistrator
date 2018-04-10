namespace Web.Application.Controllers.EmployeeRegistration.Profiles
{
    using AutoMapper;
    using Domain.Entities;
    using ViewModels;



    class EmployeeRegistrationProfile : Profile
    {
        public EmployeeRegistrationProfile()
        {
            CreateMap<EmployeeRegistration, EmployeeRegistrationViewModel>();
        }
    }
}
