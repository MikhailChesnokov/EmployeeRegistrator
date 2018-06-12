namespace Domain.Services.Time
{
    using System;



    public class TimeService : ITimeService
    {
        public DateTime Now => DateTime.Today;

        public TimeSpan WorkDayStartsAt => new TimeSpan(10, 00, 00);

        public TimeSpan WorkDayEndsAt => new TimeSpan(18, 00, 00);
    }
}