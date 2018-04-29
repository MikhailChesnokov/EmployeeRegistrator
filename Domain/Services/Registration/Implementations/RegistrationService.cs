namespace Domain.Services.Registration.Implementations
{
    using System.Collections.Generic;
    using Entities.Employee;
    using Entities.Registration;
    using Exceptions;
    using Repository;



    public class RegistrationService : IRegistrationService
    {
        public readonly IRepository<Employee> EmployeeRepository;

        public readonly IRepository<Registration> RegistrationRepository;



        public RegistrationService(
            IRepository<Registration> registrationRepository,
            IRepository<Employee> employeeRepository)
        {
            RegistrationRepository = registrationRepository;
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

            RegistrationRepository.Add(registration);
        }

        public void RegisterEmployeeLeaving(int employeeId)
        {
            Employee employee = EmployeeRepository.FindById(employeeId);

            if (employee is null)
                throw new EmployeeNotFoundException($"Employee with id \"{employeeId}\" not found.");

            Registration registration = new Registration(
                employee,
                RegistrationEventType.Leaving);

            RegistrationRepository.Add(registration);
        }

        public IEnumerable<Registration> All()
        {
            return RegistrationRepository.All();
        }
    }
}