namespace Domain.Services.Registration
{
    using System.Collections.Generic;
    using Entities.Employee;
    using Entities.Registration;



    public interface IRegistrationService : IDomainService
    {
        void RegisterEmployee(Employee employee, RegistrationEventType eventType);

        IEnumerable<Registration> All();
    }
}