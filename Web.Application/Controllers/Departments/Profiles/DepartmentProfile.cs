namespace Web.Application.Controllers.Departments.Profiles
{
    using AutoMapper;
    using Domain.Entities.Department;
    using ViewModels;



    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            CreateMap<Department, DepartmentViewModel>();
        }
    }
}