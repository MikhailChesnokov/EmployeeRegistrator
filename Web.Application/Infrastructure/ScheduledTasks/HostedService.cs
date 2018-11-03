namespace Web.Application.Infrastructure.ScheduledTasks
{
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Hosting;



    public abstract class HostedService : IHostedService
    {
        private Task _executingTask;
        private CancellationTokenSource _cancellationTokenSource;
        
        
        
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

            _executingTask = ExecuteAsync(_cancellationTokenSource.Token);

            await (_executingTask.IsCompleted ? _executingTask : Task.CompletedTask);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            if (_executingTask is null)
                await Task.CompletedTask;

            _cancellationTokenSource.Cancel();

            await Task.WhenAny(_executingTask, Task.Delay(-1, cancellationToken));
            
            cancellationToken.ThrowIfCancellationRequested();
        }

        protected abstract Task ExecuteAsync(CancellationToken cancellationToken);
    }
}