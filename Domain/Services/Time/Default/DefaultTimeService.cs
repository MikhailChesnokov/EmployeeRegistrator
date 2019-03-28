namespace Domain.Services.Time.Default
{
    using System;
    using System.Collections.Generic;
    using System.Linq;



    public sealed class DefaultTimeService : ITimeService
    {
        private readonly DateTime? _now;


        public DefaultTimeService(DefaultTimeServiceSettings settings)
        {
            WorkDayStartsAt = settings.WorkDayStartsAt;
            WorkDayEndsAt = settings.WorkDayEndsAt;
            Notification = new NotificationSettings(
                settings.Notification.RevisionPeriod,
                settings.Notification.NotifyAfter.Select(x => new NotificationLatenessSettings(x.LatenessTimeSpan)));
            _now = settings.Now;
        }
        
        
        
        public DateTime Now => _now ?? DateTime.Today;

        public TimeSpan TimeNow => _now?.TimeOfDay ?? DateTime.Now.TimeOfDay;

        public TimeSpan WorkDayStartsAt { get; }

        public TimeSpan WorkDayEndsAt { get; }

        public TimeSpan TotalWorkDayTimeSpan => WorkDayEndsAt - WorkDayStartsAt;

        public TimeSpan RevisionPeriod => Notification.RevisionPeriod;

        public IEnumerable<TimeSpan> NotifyAfter => Notification.NotifyAfter.Select(x => x.LatenessTimeSpan);


        
        private NotificationSettings Notification { get; }
    }
}