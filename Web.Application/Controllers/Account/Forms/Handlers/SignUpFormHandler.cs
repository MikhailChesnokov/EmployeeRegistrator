namespace Web.Application.Controllers.Account.Forms.Handlers
{
    using Domain.Services.User;



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
                throw new FormException("Пароли не совпадают.");

            _userService.SignUp(request.Login, request.Password);
        }
    }
}