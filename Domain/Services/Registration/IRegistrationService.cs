namespace Domain.Services.Registration
{
    public interface IRegistrationService : IDomainService
    {
        void RegisterEmployeeComing(int employeeId);

        void RegisterEmployeeLeaving(int employeeId);
    }
}