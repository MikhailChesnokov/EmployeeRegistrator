namespace Web.Application.Controllers.Employee.Forms.Handlers
{
    using Domain.Entities.Employee;
    using Domain.Services.Employee;
    using Domain.Services.Employee.Exceptions;



    public class CreateEmployeeFormHandler : IFormHandler<CreateEmployeeForm, int>
    {
        private readonly IEmployeeService _employeeService;



        public CreateEmployeeFormHandler(
            IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }



        public int Execute(CreateEmployeeForm form)
        {
            Employee employee = new Employee(
                form.FirstName,
                form.Surname,
                form.Patronymic,
                form.WorkplacePresenceRequired,
                form.PersonnelNumber);

            try
            {
                employee = _employeeService.AddEmployee(employee);
            }
            catch (EmployeeAlreadyExistsException e)
            {
                throw new FormException(e.Message);
            }

            return employee.Id;
        }
    }
}