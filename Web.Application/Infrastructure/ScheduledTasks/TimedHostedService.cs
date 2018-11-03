namespace Web.Application.Infrastructure.ScheduledTasks
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Tasks;



    public class TimedHostedService : HostedService
    {
        private readonly IEnumerable<IScheduledTask> _scheduledTasks;
        private Timer _timer;
        
        
        
        public TimedHostedService(
            IEnumerable<IScheduledTask> scheduledTasks)
        {
            _scheduledTasks = scheduledTasks;
        }

        
        
        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            _scheduledTasks.ToList().ForEach(x => _timer = new Timer(state => x.ExecuteAsync(cancellationToken), null, TimeSpan.Zero, x.Period));

            await Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}