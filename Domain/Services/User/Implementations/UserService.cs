namespace Domain.Services.User.Implementations
{
    using System;
    using System.Linq;
    using Entities.User;
    using Exceptions;
    using Repository;



    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;



        public UserService(
            IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }



        public void SignUp(string login, string password)
        {
            if (string.IsNullOrWhiteSpace(login))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(login));
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(password));

            if (_userRepository.All()?.Any(x => x.Login == login) is true)
                throw new UserAlreadyExistsException("Пользователь с таким логином уже существует");

            User user = new User(login, password);

            _userRepository.Add(user);
        }
    }
}