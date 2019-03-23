namespace Web.Application.Controllers.User.ViewModels
{
    using Departments.ViewModels;

    
    
    public class ManagerViewModel : UserViewModel
    {
        public DepartmentViewModel Department { get; set; }
    }
}