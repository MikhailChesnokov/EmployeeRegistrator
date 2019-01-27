namespace Domain.Entities.Registration
{
    using System;
    using Employee;
    using Entrance;


    public class Registration : IEntity
    {
        [Obsolete("only for reflection", true)]
        public Registration()
        {
        }

        public Registration(
            Employee employee,
            RegistrationEventType eventType,
            Entrance entrance)
        {
            SetDateTime();
            SetEventType(eventType);
            SetEmployee(employee);
            SetEntrance(entrance);
        }



        public DateTime DateTime { get; protected set; }

        public RegistrationEventType EventType { get; protected set; }

        public Employee Employee { get; protected set; }

        public Entrance Entrance { get; protected set; }

        public int Id { get; protected set; }



        private void SetDateTime()
        {
            DateTime = DateTime.Now;
        }

        private void SetEventType(RegistrationEventType eventType)
        {
            EventType = eventType;
        }

        private void SetEmployee(Employee employee)
        {
            Employee = employee ?? throw new ArgumentNullException(nameof(employee));
        }

        private void SetEntrance(Entrance entrance)
        {
            Entrance = entrance ?? throw new ArgumentNullException(nameof(entrance));
        }
    }
}