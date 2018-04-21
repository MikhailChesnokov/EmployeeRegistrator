namespace Web.Application.Controllers.Employee.Forms.Handlers
{
    using Domain.Entities.Employee;
    using Domain.Services.Employee;



    public class EditEmployeeFormHandler : IFormHandler<EditEmployeeForm>
    {
        private readonly IEmployeeService _employeeService;



        public EditEmployeeFormHandler(
            IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }



        public void Execute(EditEmployeeForm form)
        {
            Employee employee = _employeeService.GetById(form.Id);

            employee.SetFirstName(form.FirstName);
            employee.SetSurname(form.Surname);
            employee.SetPatronymic(form.Patronymic);
            employee.SetWorkplacePresenceRequirement(form.WorkplacePresenceRequired);

            _employeeService.UpdateEmployee(employee);
        }
    }
}