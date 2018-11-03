namespace Web.Application.Controllers.Departments.ViewModels
{
    using System.Collections.Generic;
    using Employee.ViewModels;



    public class DepartmentViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<EmployeeViewModel> Employees { get; set; }
    }
}