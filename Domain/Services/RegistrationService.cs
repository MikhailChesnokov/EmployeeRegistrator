namespace Domain.Services
{
    using System;
    using Entities;
    using Repository;



    public class RegistrationService : IRegistrationService
    {
        public readonly IRepository<EmployeeRegistration> _employeeRegistrationRepository;

        public readonly IRepository<Employee> _employeeRepository;



        public RegistrationService(
            IRepository<EmployeeRegistration> employeeRegistrationRepository,
            IRepository<Employee> employeeRepository)
        {
            _employeeRegistrationRepository = employeeRegistrationRepository;
            _employeeRepository = employeeRepository;
        }



        public void RegisterEmployeeComing(int employeeId)
        {
            Employee employee = _employeeRepository.FindById(employeeId);

            EmployeeRegistration registration = new EmployeeRegistration(
                employee,
                DateTime.Now,
                RegistrationEventType.Coming);

            _employeeRegistrationRepository.Add(registration);
        }

        public void RegisterEmployeeLeaving(int employeeId)
        {
            Employee employee = _employeeRepository.FindById(employeeId);

            EmployeeRegistration registration = new EmployeeRegistration(
                employee,
                DateTime.Now,
                RegistrationEventType.Leaving);

            _employeeRegistrationRepository.Add(registration);
        }
    }
}