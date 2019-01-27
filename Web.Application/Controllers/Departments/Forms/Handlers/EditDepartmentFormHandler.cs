namespace Web.Application.Controllers.Departments.Forms.Handlers
{
    using Domain.Exceptions;
    using System;
    using Domain.Services.Department;



    public class EditDepartmentFormHandler : IFormHandler<EditDepartmentForm>
    {
        private readonly IDepartmentService _departmentService;
        
        
        
        public EditDepartmentFormHandler(
            IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        
        
        public void Execute(EditDepartmentForm form)
        {
            if (!form.Id.HasValue)
                throw new Exception("Department Id required.");
            
            var department = _departmentService.GetById(form.Id.Value);

            try
            {
                _departmentService.Rename(department, form.Name);
            }
            catch (EntityAlreadyExistsException e)
            {
                throw new FormException(e.Message);
            }
        }
    }
}