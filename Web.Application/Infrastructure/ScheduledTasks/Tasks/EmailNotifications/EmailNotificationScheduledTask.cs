namespace Web.Application.Infrastructure.ScheduledTasks.Tasks.EmailNotifications
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Domain.Services.Time;
    using Microsoft.Extensions.Logging;
    using Services.MailNotification;



    public sealed class EmailNotificationScheduledTask : IScheduledTask
    {
        private readonly ITimeService _timeService;
        private readonly IMailNotificationService _notificationService;
        private readonly ILogger<EmailNotificationScheduledTask> _logger;
        
        
        
        public EmailNotificationScheduledTask(
            ILogger<EmailNotificationScheduledTask> logger,
            ITimeService timeService,
            IMailNotificationService notificationService)
        {
            _logger = logger;
            _timeService = timeService;
            _notificationService = notificationService;
        }



        public TimeSpan Period => _timeService.RevisionPeriod;
        
        

        public async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var currentTime = DateTime.Now.TimeOfDay;

            foreach (var notifyAfterSpan in _timeService.NotifyAfter)
            {
                var notifyTime = _timeService.WorkDayStartsAt + notifyAfterSpan;

                if (currentTime >= notifyTime &&
                    currentTime < notifyTime + Period)
                {
                    _logger.LogInformation("### start sending email ###");
                    
                    _notificationService.Notify(_timeService.WorkDayStartsAt);
                    
                    _logger.LogInformation("### end sending email ###");
                }
            }
            
            await Task.CompletedTask;
        }
    }
}