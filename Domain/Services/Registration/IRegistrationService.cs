namespace Domain.Services.Registration
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Entities.Employee;
    using Entities.Registration;



    public interface IRegistrationService : IDomainService
    {
        void RegisterEmployee(Employee employee, RegistrationEventType eventType);

        IQueryable<Registration> AllInclude<TProperty>(Expression<Func<Registration, TProperty>> expression);
    }
}