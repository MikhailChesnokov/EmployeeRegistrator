namespace Web.Modules
{
    using Domain.Entities.User;
    using Domain.Infrastructure.Authentication;
    using global::Autofac;



    public class AuthenticationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<IdClaimBasedCookieAuthenticationService<User>>()
                .As<IAuthenticationService<User>>()
                .InstancePerLifetimeScope()
                .WithParameter("scheme", "CookieScheme");
        }
    }
}