namespace Web.Modules
{
    using Domain.Services;
    using global::Autofac;



    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterAssemblyTypes(typeof(RegistrationService).Assembly)
                .AssignableTo<IDomainService>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}