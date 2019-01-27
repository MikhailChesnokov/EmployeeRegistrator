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
        private readonly IRepository<Registration> _registrationRepository;



        public RegistrationService(
            IRepository<Registration> registrationRepository)
        {
            _registrationRepository = registrationRepository;
        }



        public void RegisterEmployee(Employee employee, RegistrationEventType eventType, Entrance entrance)
        {
            if (employee == null)
                throw new ArgumentNullException(nameof(employee));
            if (entrance == null)
                throw new ArgumentNullException(nameof(entrance));

            var registration = new Registration(employee, eventType, entrance);

            _registrationRepository.Add(registration);
        }

        public IQueryable<Registration> AllInclude<TProperty1, TProperty2>(
            Expression<Func<Registration, TProperty1>> expression1,
            Expression<Func<Registration, TProperty2>> expression2)
        {
            return _registrationRepository.AllInclude(expression1, expression2);
        }
    }
}