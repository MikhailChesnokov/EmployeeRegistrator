namespace Domain.Services.Registration.Implementations
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Entities.Employee;
    using Entities.Entrance;
    using Entities.Registration;
    using Repository;



    public class RegistrationService : IRegistrationService
    {
        private readonly IRepository<Employee> _employeeRepository;
        private readonly IRepository<Registration> _registrationRepository;



        public RegistrationService(
            IRepository<Registration> registrationRepository,
            IRepository<Employee> employeeRepository)
        {
            _registrationRepository = registrationRepository;
            _employeeRepository = employeeRepository;
        }



        public void RegisterEmployee(Employee employee, RegistrationEventType eventType, Entrance entrance)
        {
            if (employee == null)
                throw new ArgumentNullException(nameof(employee));

            var registration = new Registration(employee, eventType, entrance);

            _registrationRepository.Add(registration);
        }

        public IQueryable<Registration> AllInclude<TProperty>(Expression<Func<Registration, TProperty>> expression)
        {
            return
                _registrationRepository
                    .AllInclude(expression)
                    .Where(x => x.DateTime.Year == DateTime.Now.Year);
        }
    }
}