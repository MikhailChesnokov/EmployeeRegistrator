namespace Web.Application.Controllers.Account.Forms.Handlers
{
    using System;
    using System.Linq;
    using Domain.Entities.User;
    using Domain.Infrastructure.Authentication;
    using Domain.Repository;



    public class SignInFormHandler : IFormHandler<SignInForm>
    {
        private readonly IAuthenticationService<User> _authenticationService;

        private readonly IRepository<User> _userRepository;



        public SignInFormHandler(
            IAuthenticationService<User> authenticationService,
            IRepository<User> userRepository)
        {
            _authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
            _userRepository = userRepository;
        }



        public void Execute(SignInForm request)
        {
            User user = _userRepository.All().SingleOrDefault(x => x.Login == request.Login);

            if (user == null)
                throw new FormException("Пользователя с таким логином не существует");

            if (!user.CheckPassword(request.Password))
                throw new FormException("Неверный пароль");

            _authenticationService.SignIn(user);
        }
    }
}