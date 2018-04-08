namespace Web.Application.Controllers.Employee.Profiles
{
    using AutoMapper;
    using Domain.Entities;
    using Forms;
    using ViewModels;



    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EditEmployeeForm>();

            CreateMap<Employee, EmployeeViewModel>();
        }
    }
}