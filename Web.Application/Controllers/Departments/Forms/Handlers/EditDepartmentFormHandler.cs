namespace Web.Application.Controllers.Departments.Forms.Handlers
{
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
            
            _departmentService.Rename(department, form.Name);
        }
    }
}