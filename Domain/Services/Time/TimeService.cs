namespace Domain.Services.Time
{
    using System;



    public class TimeService : ITimeService
    {
        public DateTime Now => DateTime.Today;

        public DateTime WorkDayStartsAt => new DateTime(2000, 1, 1, 10, 0, 0);

        public DateTime WorkDayEndsAt => new DateTime(2000, 1, 1, 18, 0, 0);
    }
}