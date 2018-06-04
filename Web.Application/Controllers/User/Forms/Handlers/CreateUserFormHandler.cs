namespace Web.Application.Controllers.User.Forms.Handlers
{
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

            User user;

            try
            {
                user = _userService.Create(form.Login, form.Password, form.Role.Value);
            }
            catch (UserAlreadyExistsException e)
            {
                throw new FormException(e.Message);
            }

            return user.Id;
        }
    }
}