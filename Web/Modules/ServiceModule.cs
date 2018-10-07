namespace Web.Modules
{
    using Application.Services.HtmlLayoutGenerator;
    using Application.Services.RegistrationsViewModel;
    using Domain.Services;
    using Domain.Services.Registration.Implementations;
    using Autofac;



    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterAssemblyTypes(typeof(RegistrationService).Assembly)
                .AssignableTo<IDomainService>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<RegistrationsViewModelService>()
                .As<IRegistrationsViewModelService>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<RazorHtmlLayoutGenerator>()
                .As<IRazorHtmlLayoutGenerator>()
                .InstancePerLifetimeScope();
        }
    }
}