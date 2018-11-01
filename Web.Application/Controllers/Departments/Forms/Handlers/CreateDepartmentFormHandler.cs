namespace Web.Application.Controllers.Departments.Forms.Handlers
{
    using Domain.Entities.Department;
    using Domain.Exceptions;
    using Domain.Services.Department;



    public class CreateDepartmentFormHandler : IFormHandler<CreateDepartmentForm, int>
    {
        private readonly IDepartmentService _departmentService;
        
        
        
        public CreateDepartmentFormHandler(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }


        
        public int Execute(CreateDepartmentForm form)
        {
            Department department = new Department(form.Name);
            
            try
            {
                department = _departmentService.Add(department);
            }
            catch (EntityAlreadyCreatedException e)
            {
                throw new FormException(e.Message);
            }

            return department.Id;
        }
    }
}