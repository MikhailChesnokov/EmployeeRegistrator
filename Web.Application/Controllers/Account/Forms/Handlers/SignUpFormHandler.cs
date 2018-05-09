namespace Web.Application.Controllers.Account.Forms.Handlers
{
    using Domain.Components.Password.Exceptions;
    using Domain.Services.User;
    using Domain.Services.User.Exceptions;



    public class SignUpFormHandler : IFormHandler<SignUpForm>
    {
        private readonly IUserService _userService;



        public SignUpFormHandler(IUserService userService)
        {
            _userService = userService;
        }



        public void Execute(SignUpForm request)
        {
            if (request.Password != request.ConfirmPassword)
                throw new FormException("Пароли не совпадают");

            try
            {
                _userService.SignUp(request.Login, request.Password);
            }
            catch (UserAlreadyExistsException e)
            {
                throw new FormException(e.Message);
            }
            catch (TooShortPasswordException e)
            {
                throw new FormException(e.Message);
            }
        }
    }
}