namespace Web.Modules
{
    using Application.Authorization.Requirements.Handlers;
    using global::Autofac;
    using Microsoft.AspNetCore.Authorization;



    public class AuthorizationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IdEqualsRequirementHandler).Assembly)
                   .AssignableTo<IAuthorizationHandler>()
                   .As<IAuthorizationHandler>()
                   .InstancePerLifetimeScope();
        }
    }
}