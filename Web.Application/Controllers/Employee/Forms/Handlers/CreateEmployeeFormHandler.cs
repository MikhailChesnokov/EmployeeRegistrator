namespace Web.Application.Controllers.Employee.Forms.Handlers
{
    using Domain.Entities;
    using Domain.Repository;



    public class CreateEmployeeFormHandler : IFormHandler<CreateEmployeeForm, int>
    {
        private readonly IRepository<Employee> _employeeRepository;



        public CreateEmployeeFormHandler(IRepository<Employee> employeeRepository1)
        {
            _employeeRepository = employeeRepository1;
        }



        public int Execute(CreateEmployeeForm form)
        {
            Employee employee = new Employee(
                form.FirstName,
                form.Surname,
                form.Patronymic);

            _employeeRepository.Add(employee);

            return employee.Id;
        }
    }
}