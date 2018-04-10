namespace Domain.Services
{
    public interface IRegistrationService : IDomainService
    {
        void RegisterEmployeeComing(int employeeId);

        void RegisterEmployeeLeaving(int employeeId);
    }
}