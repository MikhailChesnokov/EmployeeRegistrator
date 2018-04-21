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



        public UserService(IRepository<User> userRepository1)
        {
            _userRepository = userRepository1;
        }



        public void SignUp(string login, string password)
        {
            if (string.IsNullOrWhiteSpace(login))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(login));
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(password));

            if (_userRepository.All().Select(x => x.Login == login) is null)
                throw new UserAlreadyExistsException("User with the same login already exists.");

            User user = new User(login, password);

            _userRepository.Add(user);
        }
    }
}