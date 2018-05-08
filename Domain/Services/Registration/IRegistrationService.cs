namespace Domain.Services.Registration
{
    using System.Collections.Generic;
    using Entities.Employee;
    using Entities.Registration;



    public interface IRegistrationService : IDomainService
    {
        void RegisterEmployeeComing(Employee employee);

        void RegisterEmployeeLeaving(Employee employee);

        IEnumerable<Registration> All();
    }
}