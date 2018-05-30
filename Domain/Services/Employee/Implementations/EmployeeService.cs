namespace Domain.Services.Employee.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Entities.Employee;
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
                throw new EmployeeAlreadyExistsException($"Сотрудник с табельным номером \"{employee.PersonnelNumber}\" уже существует.");

            _employeeRepository.Add(employee);

            return _employeeRepository.All().SingleOrDefault(x => x.PersonnelNumber == employee.PersonnelNumber);
        }

        public void UpdateEmployee(Employee employee)
        {
            if (employee == null)
                throw new ArgumentNullException(nameof(employee));

            if (_employeeRepository.All().SingleOrDefault(x =>
                                                              x.PersonnelNumber == employee.PersonnelNumber &&
                                                              x.Id != employee.Id) != null)
                throw new EmployeeAlreadyExistsException($"Сотрудник с табельным номером \"{employee.PersonnelNumber}\" уже существует.");

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

            employee.Delete();

            UpdateEmployee(employee);
        }

        public IEnumerable<Employee> All()
        {
            return _employeeRepository.All();
        }

        public IEnumerable<Employee> AllActive()
        {
            return _employeeRepository.AllActive();
        }
    }
}