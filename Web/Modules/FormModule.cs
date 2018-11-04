namespace Web.Modules
{
    using Application.Controllers;
    using Autofac;



    public class FormModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<HardCodedFormHandlerFactory>()
                .As<IFormHandlerFactory>()
                .InstancePerLifetimeScope();
        }
    }
}