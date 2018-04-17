namespace Web.Application.Controllers.Employee.Forms.Handlers
{
    using Domain.Entities;
    using Domain.Services.Employee;



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

            employee = _employeeService.AddEmployee(employee);

            return employee.Id;
        }
    }
}