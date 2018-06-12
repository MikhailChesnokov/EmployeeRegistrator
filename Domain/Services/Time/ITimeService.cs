namespace Domain.Services.Time
{
    using System;



    public interface ITimeService : IDomainService
    {
        DateTime Now { get; }

        TimeSpan WorkDayStartsAt { get; }

        TimeSpan WorkDayEndsAt { get; }
    }
}