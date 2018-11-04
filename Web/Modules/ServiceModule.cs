namespace Web.Modules
{
    using Application.Services.HtmlLayoutGenerator;
    using Application.Services.MailNotification;
    using Application.Services.RegistrationsViewModel;
    using Domain.Services;
    using Domain.Services.Registration.Implementations;
    using Autofac;
    using Domain.Services.Time;
    using Domain.Services.Time.Default;
    using Microsoft.Extensions.Configuration;



    public class ServiceModule : ConfiguredModule
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<DefaultTimeService>()
                .As<ITimeService>()
                .WithParameter("settings", ConfigurationRoot.GetSection("Time").Get<DefaultTimeServiceSettings>())
                .InstancePerLifetimeScope();
            
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

            builder
                .RegisterType<MailNotificationService>()
                .As<IMailNotificationService>()
                .InstancePerLifetimeScope();
        }
    }
}