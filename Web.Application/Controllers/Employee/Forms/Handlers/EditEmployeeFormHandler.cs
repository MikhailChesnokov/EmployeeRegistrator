namespace Web.Application.Controllers.Employee.Forms.Handlers
{
    using Domain.Entities.Employee;
    using Domain.Services.Department;
    using Domain.Services.Employee;
    using Domain.Services.Employee.Exceptions;



    public class EditEmployeeFormHandler : IFormHandler<EditEmployeeForm>
    {
        private readonly IEmployeeService _employeeService;
        private readonly IDepartmentService _departmentService;



        public EditEmployeeFormHandler(
            IEmployeeService employeeService,
            IDepartmentService departmentService)
        {
            _employeeService = employeeService;
            _departmentService = departmentService;
        }



        public void Execute(EditEmployeeForm form)
        {
            if (!form.DepartmentId.HasValue)
                throw new FormException("Отдел не выбран.");
            
            var department = _departmentService.GetById(form.DepartmentId.Value);
            
            Employee employee = _employeeService.GetById(form.Id);

            employee.SetFirstName(form.FirstName);
            employee.SetSurname(form.Surname);
            employee.SetPatronymic(form.Patronymic);
            employee.SetWorkplacePresenceRequirement(form.WorkplacePresenceRequired);
            employee.SetPersonnelNumber(form.PersonnelNumber);
            if (department.Id != employee.Department.Id)
            {
                var oldDepartment = employee.Department;
                
                oldDepartment.RemoveEmployee(employee);
                department.AddEmployee(employee);
                
                employee.SetDepartment(department);
                
                _departmentService.Update(department);
                _departmentService.Update(oldDepartment);
            }

            try
            {
                _employeeService.UpdateEmployee(employee);
            }
            catch (EmployeeAlreadyExistsException e)
            {
                throw new FormException(e.Message);
            }
        }
    }
}