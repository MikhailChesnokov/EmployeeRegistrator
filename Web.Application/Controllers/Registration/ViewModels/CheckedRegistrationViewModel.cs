namespace Web.Application.Controllers.Registration.ViewModels
{
    using System;
    using Enums;



    public class CheckedRegistrationViewModel
    {
        public RegistrationViewModel Registration { get; set; }

        public RegistrationCheckResult CheckResult { get; set; }

        public TimeSpan WorkPeriodTime { get; set; }
    }
}