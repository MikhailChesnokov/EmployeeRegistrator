namespace Web.Modules
{
    using Application.Services.Mail;
    using Application.Services.Mail.MailKit;
    using Autofac;
    using Microsoft.Extensions.Configuration;



    public class MailModule : ConfiguredModule
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<MailKitService>()
                .As<IMailService>()
                .WithParameter("settings", ConfigurationRoot.GetSection("Mail").Get<MailKitSettings>())
                .SingleInstance();
        }
    }
}