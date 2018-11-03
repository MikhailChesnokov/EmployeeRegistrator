namespace Web.Modules
{
    using Application.Infrastructure.ScheduledTasks.Tasks;
    using Application.Infrastructure.ScheduledTasks.Tasks.EmailNotifications;
    using Autofac;
    using Microsoft.Extensions.Configuration;



    public class ScheduledTasksModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<EmailNotificationScheduledTask>()
                .As<IScheduledTask>()
                .InstancePerLifetimeScope();
        }
    }
}