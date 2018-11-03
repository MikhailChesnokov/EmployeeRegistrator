namespace Domain.Services.Time.Default {
    using System;
    using System.Collections.Generic;



    public sealed class NotificationSettings
    {
        internal NotificationSettings(TimeSpan revisionPeriod, IEnumerable<NotificationLatenessSettings> notifyAfter)
        {
            RevisionPeriod = revisionPeriod;
            NotifyAfter = notifyAfter;
        }
        
        
        
        public TimeSpan RevisionPeriod { get; }

        public IEnumerable<NotificationLatenessSettings> NotifyAfter { get; }
    }
}