namespace Web.Modules
{
    using global::Autofac;
    using Microsoft.AspNetCore.Http;



    public class AspNetCoreServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<HttpContextAccessor>()
                .As<IHttpContextAccessor>()
                .SingleInstance();
        }
    }
}