namespace Web.Application.Controllers.User.Forms.Handlers
{
    using System;
    using Domain.Entities.User;
    using Domain.Services.Department;
    using Domain.Services.Entrance;
    using Domain.Services.User;
    using Domain.Services.User.Exceptions;

    
    
    public class EditUserFormHandler : IFormHandler<EditUserForm>
    {
        private readonly IUserService _userService;
        private readonly IEntranceService _entranceService;
        private readonly IDepartmentService _departmentService;

        
        
        public EditUserFormHandler(
            IUserService userService,
            IEntranceService entranceService,
            IDepartmentService departmentService)
        {
            _userService = userService;
            _entranceService = entranceService;
            _departmentService = departmentService;
        }

        
        
        public void Execute(EditUserForm form)
        {
            if (form.Role is null)
                throw new FormException("Роль не выбрана.");
            
            if (form.Role.HasValue && form.Role.Value == Role.SecurityGuard && !form.EntranceId.HasValue)
                throw new FormException("Вход в здание не выбран.");
            
            if (form.Role.HasValue && form.Role.Value == Role.Manager && !form.DepartmentId.HasValue)
                throw new FormException("Отдел не выбран.");
            
            if (form.Role.HasValue && form.Role.Value == Role.Administrator)
            {
                if (form.NeedNotify && string.IsNullOrWhiteSpace(form.Email))
                {
                    throw new FormException("Для получения уведомлений должен быть указан адрес электронной почты.");
                }
            }
            
            var user = _userService.GetById(form.Id);

            try
            {
                _userService.ChangeLogin(user, form.Login);
            }
            catch (UserAlreadyExistsException e)
            {
                throw new FormException(e.Message);
            }

            if (form.Role.HasValue)
            {
                user.ChangeRole(form.Role.Value);

                switch (form.Role)
                {
                    case Role.Administrator:
                    {
                        user.ChangeNotification(form.Email, form.NeedNotify);
                        break;
                    }
                    case Role.SecurityGuard when user is SecurityGuard securityGuard && form.EntranceId.HasValue:
                    {
                        var entrance = _entranceService.GetById(form.EntranceId.Value);
                    
                        securityGuard.ChangeEntrance(entrance);
                        break;
                    }
                    case Role.Manager when user is Manager manager && form.DepartmentId.HasValue:
                    {
                        var department = _departmentService.GetById(form.DepartmentId.Value);
                        
                        manager.ChangeDepartment(department);
                        
                        manager.ChangeNotification(form.Email, false);
                        break;
                    }
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}