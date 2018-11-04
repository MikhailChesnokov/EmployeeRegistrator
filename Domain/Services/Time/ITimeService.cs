namespace Domain.Services.Time
{
    using System;
    using System.Collections.Generic;



    public interface ITimeService
    {
        DateTime Now { get; }

        TimeSpan TimeNow { get; }

        TimeSpan WorkDayStartsAt { get; }

        TimeSpan WorkDayEndsAt { get; }

        TimeSpan TotalWorkDayTimeSpan { get; }


        TimeSpan RevisionPeriod { get; }

        IEnumerable<TimeSpan> NotifyAfter { get; }
    }
}