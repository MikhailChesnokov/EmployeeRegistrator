namespace Domain.Entities
{
    using System;



    public class EmployeeRegistration : IEntity
    {
        [Obsolete("only for reflection", true)]
        public EmployeeRegistration() { }

        public EmployeeRegistration(
            Employee employee,
            DateTime dateTime,
            RegistrationEventType eventType)
        {
            SetDateTime(dateTime);
            SetEventType(eventType);
            SetEmployee(employee);
        }



        public DateTime DateTime { get; protected set; }

        public RegistrationEventType EventType { get; protected set; }

        public Employee Employee { get; protected set; }

        public int Id { get; protected set; }



        private void SetDateTime(DateTime dateTime)
        {
            DateTime = dateTime;
        }

        private void SetEventType(RegistrationEventType eventType)
        {
            EventType = eventType;
        }

        private void SetEmployee(Employee employee)
        {
            Employee = employee ?? throw new ArgumentNullException(nameof(employee));
        }
    }
}