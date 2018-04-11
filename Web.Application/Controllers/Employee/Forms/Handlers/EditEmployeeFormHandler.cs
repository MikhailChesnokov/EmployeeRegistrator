namespace Web.Application.Controllers.Employee.Forms.Handlers
{
    using Domain.Entities;
    using Domain.Repository;



    public class EditEmployeeFormHandler : IFormHandler<EditEmployeeForm>
    {
        private readonly IRepository<Employee> _employeeRepository;



        public EditEmployeeFormHandler(IRepository<Employee> employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }



        public void Execute(EditEmployeeForm form)
        {
            Employee employee = _employeeRepository.FindById(form.Id);

            employee.SetFirstName(form.FirstName);
            employee.SetSurname(form.Surname);
            employee.SetPatronymic(form.Patronymic);

            _employeeRepository.Update(employee);
        }
    }
}