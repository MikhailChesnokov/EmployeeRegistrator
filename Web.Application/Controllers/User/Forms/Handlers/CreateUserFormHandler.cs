namespace Web.Application.Controllers.User.Forms.Handlers
{
    using Domain.Components.Password.Exceptions;
    using Domain.Entities.User;
    using Domain.Services.User;
    using Domain.Services.User.Exceptions;



    public class CreateUserFormHandler : IFormHandler<CreateUserForm, int>
    {
        private readonly IUserService _userService;



        public CreateUserFormHandler(
            IUserService userService)
        {
            _userService = userService;
        }



        public int Execute(CreateUserForm form)
        {
            if (form.Role is null)
                throw new FormException("Роль не выбрана.");

            if (string.IsNullOrWhiteSpace(form.Password))
                throw new FormException("Пароль не указан.");

            if (string.IsNullOrWhiteSpace(form.ConfirmPassword))
                throw new FormException("Подтверждение пароля не указано.");

            if (!form.Password.Equals(form.ConfirmPassword))
                throw new FormException("Пароли не совпадают.");
            
            if (form.Role.HasValue && form.Role.Value == Role.SecurityGuard && !form.EntranceId.HasValue)
                throw new FormException("Вход в здание не выбран.");

            User user;

            try
            {
                user = _userService.Create(form.Login, form.Password, form.Role.Value, form.Email, form.NeedNotify ?? false, form.EntranceId);
            }
            catch (UserAlreadyExistsException e)
            {
                throw new FormException(e.Message);
            }
            catch (TooShortPasswordException e)
            {
                throw new FormException(e.Message);
            }

            return user.Id;
        }
    }
}