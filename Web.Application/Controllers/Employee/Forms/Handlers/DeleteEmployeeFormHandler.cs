namespace Web.Application.Controllers.Employee.Forms.Handlers
{
    using Domain.Services.Employee;



    public class DeleteEmployeeFormHandler : IFormHandler<DeleteEmployeeForm>
    {
        private readonly IEmployeeService _employeeService;



        public DeleteEmployeeFormHandler(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }



        public void Execute(DeleteEmployeeForm form)
        {
            _employeeService.Delete(form.Id);
        }
    }
}