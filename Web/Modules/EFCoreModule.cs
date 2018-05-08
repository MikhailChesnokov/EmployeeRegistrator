namespace Web.Modules
{
    using Domain.Infrastructure.Repository;
    using Domain.Repository;
    using global::Autofac;
    using Microsoft.EntityFrameworkCore;



    public class EfCoreModule : Module
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
                .SingleInstance();
        }
    }
}