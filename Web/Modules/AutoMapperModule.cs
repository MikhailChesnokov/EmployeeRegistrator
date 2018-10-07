namespace Web.Modules
{
    using Application.Controllers.Employee.Profiles;
    using AutoMapper;
    using Autofac;



    public class AutoMapperModule : Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            containerBuilder
                .Register(c => new MapperConfiguration(options => options.AddProfiles(typeof(EmployeeProfile).Assembly))
                              .CreateMapper())
                .As<IMapper>()
                .SingleInstance();
        }
    }
}