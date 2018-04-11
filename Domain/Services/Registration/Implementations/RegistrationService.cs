namespace Domain.Services.Registration.Implementations
{
    using Entities;
    using Exceptions;
    using Repository;



    public class RegistrationService : IRegistrationService
    {
        public readonly IRepository<Registration> EmployeeRegistrationRepository;

        public readonly IRepository<Employee> EmployeeRepository;



        public RegistrationService(
            IRepository<Registration> employeeRegistrationRepository,
            IRepository<Employee> employeeRepository)
        {
            EmployeeRegistrationRepository = employeeRegistrationRepository;
            EmployeeRepository = employeeRepository;
        }



        public void RegisterEmployeeComing(int employeeId)
        {
            Employee employee = EmployeeRepository.FindById(employeeId);

            if (employee is null)
                throw new EmployeeNotFoundException($"Employee with id \"{employeeId}\" not found.");

            Registration registration = new Registration(
                employee,
                RegistrationEventType.Coming);

            EmployeeRegistrationRepository.Add(registration);
        }

        public void RegisterEmployeeLeaving(int employeeId)
        {
            Employee employee = EmployeeRepository.FindById(employeeId);

            if (employee is null)
                throw new EmployeeNotFoundException($"Employee with id \"{employeeId}\" not found.");

            Registration registration = new Registration(
                employee,
                RegistrationEventType.Leaving);

            EmployeeRegistrationRepository.Add(registration);
        }
    }
}