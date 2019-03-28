namespace Domain.Services.Time.Default
{
    using System;
    using System.Collections.Generic;



    public class DefaultTimeServiceSettings
    {
        public DateTime? Now { get; set; }

        public TimeSpan WorkDayStartsAt { get; set; }

        public TimeSpan WorkDayEndsAt { get; set; }

        public DefaultTimeServiceNotificationSettings Notification { get; set; }
    }

    public class DefaultTimeServiceNotificationSettings
    {
        public TimeSpan RevisionPeriod { get; set; }

        public IEnumerable<DefaultTimeServiceNotificationLatenessSettings> NotifyAfter { get; set; }
    }

    public class DefaultTimeServiceNotificationLatenessSettings
    {
        public TimeSpan LatenessTimeSpan { get; set; }
    }
}