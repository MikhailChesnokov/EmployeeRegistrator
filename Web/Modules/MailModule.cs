namespace Web.Modules
{
    using Application.Infrastructure.Mail;
    using Application.Infrastructure.Mail.MailKit;
    using Autofac;
    using Microsoft.Extensions.Configuration;



    public class MailModule : ConfiguredModule
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<MailKitBasedMailService>()
                .As<IMailService>()
                .WithParameter("settings", ConfigurationRoot.GetSection("Mail:Smtp").Get<MailKitBasedMailServiceSettings>())
                .SingleInstance();
        }
    }
}