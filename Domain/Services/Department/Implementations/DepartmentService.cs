namespace Domain.Services.Department.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Entities.Department;
    using Exceptions;
    using Repository;



    public class DepartmentService : IDepartmentService
    {
        private readonly IRepository<Department> _departmentRepository;
        
        
        
        public DepartmentService(
            IRepository<Department> departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }


        
        public Department Add(Department department)
        {
            CheckForSameDepartment(department, department.Name);

            _departmentRepository.Add(department);

            return
                _departmentRepository
                    .AllActiveInclude(x => x.Employees)
                    .Single(x => x.Name == department.Name);
        }

        public void Update(Department department)
        {
            CheckForSameDepartment(department, department.Name);

            _departmentRepository.Update(department);
        }

        public Department GetById(int id)
        {
            return
                _departmentRepository
                    .AllActiveInclude(x => x.Employees)
                    .FirstOrDefault(x => x.Id == id);
        }

        public void Delete(int id)
        {
            var department = _departmentRepository.FindById(id);

            if (department is null)
                throw new ArgumentException("Department not found.");

            department.Delete();

            _departmentRepository.Update(department);
        }

        public IEnumerable<Department> All()
        {
            return _departmentRepository.AllInclude(x => x.Employees);
        }

        public IEnumerable<Department> AllActive()
        {
            return _departmentRepository.AllActiveInclude(x => x.Employees);
        }

        
        
        public void Rename(Department department, string name)
        {
            CheckForSameDepartment(department, name);
            
            department.Rename(name);
            
            _departmentRepository.Update(department);
        }

        private void CheckForSameDepartment(Department department, string name)
        {
            if (department == null)
                throw new ArgumentNullException(nameof(department));

            if (_departmentRepository
                .AllActiveInclude(x => x.Employees)
                .Any(x =>
                    x.Id != department.Id &&
                    string.Equals(x.Name, name, StringComparison.InvariantCultureIgnoreCase)))
            {
                throw new EntityAlreadyExistsException($"Отдел с именем '{name}' уже существует.");
            }
        }
    }
}