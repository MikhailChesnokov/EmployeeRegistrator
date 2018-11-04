namespace Web.Modules
{
    using Application.Authorization.Requirements.Handlers;
    using Autofac;
    using Microsoft.AspNetCore.Authorization;



    public class AuthorizationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(RoleRequirementHandler).Assembly)
                   .AssignableTo<IAuthorizationHandler>()
                   .As<IAuthorizationHandler>()
                   .InstancePerLifetimeScope();
        }
    }
}