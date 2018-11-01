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
            if (department == null)
                throw new ArgumentNullException(nameof(department));

            if (_departmentRepository.All().SingleOrDefault(x => x.Name == department.Name) != null)
                throw new EntityAlreadyCreatedException($"Отдел с именем \"{department.Name}\" уже существует.");

            _departmentRepository.Add(department);

            return _departmentRepository.All().SingleOrDefault(x => x.Name == department.Name);
        }

        public void Update(Department department)
        {
            if (department == null)
                throw new ArgumentNullException(nameof(department));

            if (_departmentRepository.All().SingleOrDefault(x =>
                                                              x.Name == department.Name &&
                                                              x.Id != department.Id) != null)
                throw new EntityAlreadyCreatedException($"Отдел с именем \"{department.Name}\" уже существует.");

            _departmentRepository.Update(department);
        }

        public Department GetById(int id)
        {
            return _departmentRepository.FindById(id);
        }

        public void Delete(int id)
        {
            Department department = _departmentRepository.FindById(id);

            if (department is null)
                throw new ArgumentException("Department not found.");

            department.Delete();

            Update(department);
        }

        public IEnumerable<Department> All()
        {
            return _departmentRepository.All();
        }

        public IEnumerable<Department> AllActive()
        {
            return _departmentRepository.AllActive();
        }
    }
}