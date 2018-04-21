namespace Domain.Services.Employee
{
    using Entities.Employee;



    public interface IEmployeeService : IDomainService
    {
        Employee AddEmployee(Employee employee);

        void UpdateEmployee(Employee employee);

        Employee GetById(int id);

        void Delete(int id);
    }
}