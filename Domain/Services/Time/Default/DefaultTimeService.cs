namespace Domain.Services.Time.Default
{
    using System;
    using System.Collections.Generic;
    using System.Linq;



    public sealed class DefaultTimeService : ITimeService
    {
        public DefaultTimeService(DefaultTimeServiceSettings settings)
        {
            WorkDayStartsAt = settings.WorkDayStartsAt;
            WorkDayEndsAt = settings.WorkDayEndsAt;
            Notification = new NotificationSettings(
                settings.Notification.RevisionPeriod,
                settings.Notification.NotifyAfter.Select(x => new NotificationLatenessSettings(x.LatenessTimeSpan)));
        }
        
        
        
        public DateTime Now => DateTime.Today;

        public TimeSpan TimeNow => DateTime.Now.TimeOfDay;

        public TimeSpan WorkDayStartsAt { get; }

        public TimeSpan WorkDayEndsAt { get; }

        public TimeSpan TotalWorkDayTimeSpan => WorkDayEndsAt - WorkDayStartsAt;

        public TimeSpan RevisionPeriod => Notification.RevisionPeriod;

        public IEnumerable<TimeSpan> NotifyAfter => Notification.NotifyAfter.Select(x => x.LatenessTimeSpan);


        
        private NotificationSettings Notification { get; }
    }
}