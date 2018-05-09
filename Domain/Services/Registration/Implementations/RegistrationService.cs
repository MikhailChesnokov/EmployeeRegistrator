namespace Domain.Services.Registration.Implementations
{
    using System;
    using System.Collections.Generic;
    using Entities.Employee;
    using Entities.Registration;
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



        public void RegisterEmployee(Employee employee, RegistrationEventType eventType)
        {
            if (employee == null)
                throw new ArgumentNullException(nameof(employee));

            Registration registration = new Registration(employee, eventType);

            RegistrationRepository.Add(registration);
        }

        public IEnumerable<Registration> All()
        {
            return RegistrationRepository.All();
        }
    }
}