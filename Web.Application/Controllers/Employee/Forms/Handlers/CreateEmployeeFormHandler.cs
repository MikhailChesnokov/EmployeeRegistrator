namespace Web.Application.Controllers.Employee.Forms.Handlers
{
    using Domain.Entities.Employee;
    using Domain.Services.Department;
    using Domain.Services.Employee;
    using Domain.Services.Employee.Exceptions;



    public class CreateEmployeeFormHandler : IFormHandler<CreateEmployeeForm, int>
    {
        private readonly IEmployeeService _employeeService;
        private readonly IDepartmentService _departmentService;



        public CreateEmployeeFormHandler(
            IEmployeeService employeeService,
            IDepartmentService departmentService)
        {
            _employeeService = employeeService;
            _departmentService = departmentService;
        }



        public int Execute(CreateEmployeeForm form)
        {
            if (!form.DepartmentId.HasValue)
                throw new FormException("Отдел не выбран.");
            
            var department = _departmentService.GetById(form.DepartmentId.Value);
            
            Employee employee = new Employee(
                form.FirstName,
                form.Surname,
                form.Patronymic,
                form.WorkplacePresenceRequired,
                form.PersonnelNumber,
                department);

            try
            {
                employee = _employeeService.AddEmployee(employee);
            }
            catch (EmployeeAlreadyExistsException e)
            {
                throw new FormException(e.Message);
            }

            department.AddEmployee(employee);
            
            return employee.Id;
        }
    }
}