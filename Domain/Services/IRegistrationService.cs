namespace Domain.Services
{
    internal interface IRegistrationService : IDomainService
    {
        void RegisterEmployeeComing(int employeeId);

        void RegisterEmployeeLeaving(int employeeId);
    }
}