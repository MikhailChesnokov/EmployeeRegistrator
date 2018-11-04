namespace Domain.Services.Time.Default {
    using System;



    public sealed class NotificationLatenessSettings
    {
        internal NotificationLatenessSettings(TimeSpan latenessTimeSpan)
        {
            LatenessTimeSpan = latenessTimeSpan;
        }
        
        
        
        public TimeSpan LatenessTimeSpan { get; }
    }
}