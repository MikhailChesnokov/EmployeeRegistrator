namespace Web.Application.Controllers.Employee.Forms.Handlers
{
    using Domain.Entities;
    using Domain.Repository;



    public class DeleteEmployeeFormHandler : IFormHandler<DeleteEmployeeForm>
    {
        private readonly IRepository<Employee> _employeeRepository;



        public DeleteEmployeeFormHandler(IRepository<Employee> employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }



        public void Execute(DeleteEmployeeForm form)
        {
            Employee employee = _employeeRepository.FindById(form.Id);

            if (employee is null) return;

            _employeeRepository.Delete(employee);
        }
    }
}