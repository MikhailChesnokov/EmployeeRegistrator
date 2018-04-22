namespace Web.Application.Controllers.Registration.ViewModels
{
    using System;



    public class RowViewModel
    {
        public RowViewModel(
            RegistrationViewModel registration,
            bool daySpaned,
            int daySpanedCount,
            DateTime dayDate,
            bool employeeSpaned,
            int employeeSpanedCount,
            string employeeFio,
            bool isCorrect,
            TimeSpan? workTime = null)
        {
            Registration = registration;
            DaySpaned = daySpaned;
            DaySpanedCount = daySpanedCount;
            DayDate = dayDate;
            EmployeeSpaned = employeeSpaned;
            EmployeeSpanedCount = employeeSpanedCount;
            EmployeeFio = employeeFio;
            IsCorrectRow = isCorrect;
            WorkTime = workTime;
        }

        public RegistrationViewModel Registration { get; set; }

        public bool DaySpaned { get; set; }

        public int DaySpanedCount { get; set; }

        public DateTime DayDate { get; set; }

        public bool EmployeeSpaned { get; set; }

        public int EmployeeSpanedCount { get;set; }

        public string EmployeeFio { get; set; }

        public bool IsCorrectRow { get; set; }

        public TimeSpan? WorkTime { get;set; }
    }
}