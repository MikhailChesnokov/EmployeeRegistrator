namespace Domain.Services.Registration
{
    using System.Collections.Generic;
    using Entities.Registration;



    public interface IRegistrationService : IDomainService
    {
        void RegisterEmployeeComing(int employeeId);

        void RegisterEmployeeLeaving(int employeeId);

        IEnumerable<Registration> All();
    }
}