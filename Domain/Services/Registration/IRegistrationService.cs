namespace Domain.Services.Registration
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Entities.Employee;
    using Entities.Entrance;
    using Entities.Registration;



    public interface IRegistrationService : IDomainService
    {
        void RegisterEmployee(Employee employee, RegistrationEventType eventType, Entrance entrance);

        IQueryable<Registration> AllInclude<TProperty>(Expression<Func<Registration, TProperty>> expression);
    }
}