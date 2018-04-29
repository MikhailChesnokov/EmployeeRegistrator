namespace Web.Application.Controllers.Employee.Forms.Handlers
{
    using Domain.Entities.Employee;
    using Domain.Services.Employee;
    using Domain.Services.Employee.Exceptions;



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
            employee.SetPersonnelNumber(form.PersonnelNumber);

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