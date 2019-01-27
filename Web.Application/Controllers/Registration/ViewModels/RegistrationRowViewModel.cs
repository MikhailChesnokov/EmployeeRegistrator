namespace Web.Application.Controllers.Registration.ViewModels
{
    using System;
    using Domain.Entities.Registration;
    using Enums;



    public class RegistrationRowViewModel
    {
        public TimeSpan Time { get; set; }

        public RegistrationEventType Event { get; set; }

        public RegistrationCheckResult CheckResult { get; set; }

        public TimeSpan WorkTimeInterval { get; set; }

        public string EntranceCompleteName { get; set; }
    }
}