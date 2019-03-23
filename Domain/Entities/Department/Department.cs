namespace Domain.Entities.Department
{
    using System;
    using System.Collections.Generic;
    using Employee;
    using User;


    public class Department : IRemovableEntity
    {
        [Obsolete("Only for reflection", true)]
        public Department() { }

        public Department(string name)
        {
            Rename(name);
        }
        
        
        
        public int Id { get; protected set; }

        public string Name { get; protected set; }

        public DateTime? DeletedAtUtc { get; protected set; }

        public IList<Employee> Employees { get; protected set; }
        
        public IList<Manager> Managers { get; protected set; }


        
        protected internal void Rename(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));

            Name = name;
        }
        
        public void AddEmployee(Employee employee)
        {
            Employees.Add(employee);
        }

        public void RemoveEmployee(Employee employee)
        {
            Employees.Add(employee);
        }

        public bool IsDeleted()
        {
            return DeletedAtUtc.HasValue;
        }

        public void Delete()
        {
            DeletedAtUtc = DateTime.UtcNow;
        }
    }
}