namespace Domain.Services.Time
{
    using System;



    public interface ITimeService : IDomainService
    {
        DateTime Now { get; }

        TimeSpan TimeNow { get; }

        TimeSpan WorkDayStartsAt { get; }

        TimeSpan WorkDayEndsAt { get; }

        TimeSpan TotalWorkDayTimeSpan { get; }
    }
}