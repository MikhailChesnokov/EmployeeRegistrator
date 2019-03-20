namespace Web.Application.Controllers.User.Forms.Handlers
{
    using Domain.Entities.User;
    using Domain.Services.Entrance;
    using Domain.Services.User;
    using Domain.Services.User.Exceptions;

    public class EditUserFormHandler : IFormHandler<EditUserForm>
    {
        private readonly IUserService _userService;
        private readonly IEntranceService _entranceService;

        
        
        public EditUserFormHandler(
            IUserService userService,
            IEntranceService entranceService)
        {
            _userService = userService;
            _entranceService = entranceService;
        }

        
        
        public void Execute(EditUserForm form)
        {
            if (form.Role is null)
                throw new FormException("Роль не выбрана.");
            
            if (form.Role.HasValue && form.Role.Value == Role.SecurityGuard && !form.EntranceId.HasValue)
                throw new FormException("Вход в здание не выбран.");
            
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

                if (form.Role.Value == Role.Administrator)
                {
                    user.ChangeNotification(form.Email, form.NeedNotify);
                }

                if (form.Role == Role.SecurityGuard && user is SecurityGuard securityGuard && form.EntranceId.HasValue)
                {
                    var entrance = _entranceService.GetById(form.EntranceId.Value);
                    
                    securityGuard.ChangeEntrance(entrance);
                }
            }
        }
    }
}