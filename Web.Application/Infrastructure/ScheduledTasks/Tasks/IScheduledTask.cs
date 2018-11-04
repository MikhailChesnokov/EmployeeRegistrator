namespace Web.Application.Infrastructure.ScheduledTasks.Tasks
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;



    public interface IScheduledTask
    {
        TimeSpan Period { get; }
        
        Task ExecuteAsync(CancellationToken cancellationToken);
    }
}