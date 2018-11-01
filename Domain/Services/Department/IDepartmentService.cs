namespace Domain.Services.Department
{
    using System.Collections.Generic;
    using Entities.Department;



    public interface IDepartmentService : IDomainService
    {
        Department Add(Department department);

        void Update(Department department);

        Department GetById(int id);

        void Delete(int id);
        
        IEnumerable<Department> All();

        IEnumerable<Department> AllActive();
    }
}