namespace Domain.Services.Time
{
    using System;



    public interface ITimeService : IDomainService
    {
        DateTime Now { get; }

        DateTime WorkDayStartsAt { get; }

        DateTime WorkDayEndsAt { get; }
    }
}