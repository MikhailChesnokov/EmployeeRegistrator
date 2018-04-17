namespace Domain.Services.Employee.Implementations
{
    using System;
    using System.Linq;
    using Entities;
    using Exceptions;
    using Repository;



    public class EmployeeService : IEmployeeService
    {
        private readonly IRepository<Employee> _employeeRepository;



        public EmployeeService(IRepository<Employee> employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }



        public Employee AddEmployee(Employee employee)
        {
            if (employee == null)
                throw new ArgumentNullException(nameof(employee));

            if (_employeeRepository.All().SingleOrDefault(x => x.PersonnelNumber == employee.PersonnelNumber) != null)
                throw new EmployeeAlreadyExistsException($"Employee with the personnel number \"{employee.PersonnelNumber}\" already exists.");

            _employeeRepository.Add(employee);

            return _employeeRepository.All().SingleOrDefault(x => x.PersonnelNumber == employee.PersonnelNumber);
        }

        public void UpdateEmployee(Employee employee)
        {
            if (employee == null)
                throw new ArgumentNullException(nameof(employee));

            if (_employeeRepository.All().SingleOrDefault(x => x.PersonnelNumber == employee.PersonnelNumber) != null)
                throw new EmployeeAlreadyExistsException($"Employee with the personnel number \"{employee.PersonnelNumber}\" already exists.");

            _employeeRepository.Update(employee);
        }

        public Employee GetById(int id)
        {
            return _employeeRepository.FindById(id);
        }

        public void Delete(int id)
        {
            Employee employee = _employeeRepository.FindById(id);

            if (employee is null)
                throw new ArgumentException("Employee not found.");

            _employeeRepository.Delete(employee);
        }
    }
}