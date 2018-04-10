namespace Domain.Services
{
    using Entities;
    using Repository;



    public class RegistrationService : IRegistrationService
    {
        public readonly IRepository<EmployeeRegistration> EmployeeRegistrationRepository;

        public readonly IRepository<Employee> EmployeeRepository;



        public RegistrationService(
            IRepository<EmployeeRegistration> employeeRegistrationRepository,
            IRepository<Employee> employeeRepository)
        {
            EmployeeRegistrationRepository = employeeRegistrationRepository;
            EmployeeRepository = employeeRepository;
        }



        public void RegisterEmployeeComing(int employeeId)
        {
            Employee employee = EmployeeRepository.FindById(employeeId);

            EmployeeRegistration registration = new EmployeeRegistration(
                employee,
                RegistrationEventType.Coming);

            EmployeeRegistrationRepository.Add(registration);
        }

        public void RegisterEmployeeLeaving(int employeeId)
        {
            Employee employee = EmployeeRepository.FindById(employeeId);

            EmployeeRegistration registration = new EmployeeRegistration(
                employee,
                RegistrationEventType.Leaving);

            EmployeeRegistrationRepository.Add(registration);
        }
    }
}