namespace Web.Application.Controllers.Departments.Forms.Handlers
{
    using Domain.Exceptions;
    using Domain.Services.Department;



    public class DeleteDepartmentFormHandler : IFormHandler<DeleteDepartmentForm>
    {
        private readonly IDepartmentService _departmentService;
        
        
        public DeleteDepartmentFormHandler(
            IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        
        
        public void Execute(DeleteDepartmentForm form)
        {
            if (!form.Id.HasValue)
                throw new FormException("Department id required.");

            try
            {
                _departmentService.Delete(form.Id.Value);
            }
            catch (CannotDeleteEntityInUseException e)
            {
                throw new FormException("Невозможно удалить отдел, так как на него ссылаются один или несколько сотрудников.");
            }
        }
    }
}