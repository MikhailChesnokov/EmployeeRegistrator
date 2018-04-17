namespace Domain.Services.Employee
{
    using Entities;



    public interface IEmployeeService : IDomainService
    {
        Employee AddEmployee(Employee employee);

        void UpdateEmployee(Employee employee);

        Employee GetById(int id);

        void Delete(int id);
    }
}