namespace Web.Modules
{
    using Domain.Infrastructure.Repository;
    using Domain.Repository;
    using Autofac;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;



    public class RepositoryModule : ConfiguredModule
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterGeneric(typeof(EntityFrameworkCoreRepository<>))
                .As(typeof(IRepository<>))
                .InstancePerLifetimeScope();

            builder
                .RegisterType<DefaultContext>()
                .As<DbContext>()
                .WithParameter("settings", ConfigurationRoot.GetSection("Persistence").Get<EntityFrameworkSettings>())
                .SingleInstance();
        }
    }
}