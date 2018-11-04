namespace Web.Application.Controllers.Employee.ViewModels
{
    using System.Collections.Generic;
    using Departments.ViewModels;



    public class EmployeeViewModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string Surname { get; set; }

        public string Patronymic { get; set; }

        public string Fio { get; set; }

        public string PersonnelNumber { get; set; }

        public DepartmentViewModel Department { get; set; }

        public bool WorkplacePresenceRequired { get; set; }
    }



    public class EmployeeViewModelEqualityComparer : IEqualityComparer<EmployeeViewModel>
    {
        public bool Equals(EmployeeViewModel x, EmployeeViewModel y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode(EmployeeViewModel obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}