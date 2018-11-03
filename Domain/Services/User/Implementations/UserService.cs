namespace Domain.Services.User.Implementations
{
    using System;
    using System.Collections.Generic;
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

            User user = new Administrator(login, password);

            _userRepository.Add(user);
        }

        public User Create(string login, string password, Roles role, string email = null, bool needNotify = false)
        {
            if (string.IsNullOrWhiteSpace(login))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(login));
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(password));

            if (_userRepository.All()?.Any(x => x.Login == login) is true)
                throw new UserAlreadyExistsException("Пользователь с таким логином уже существует");

            User user;

            switch (role)
            {
                case Roles.Administrator:
                    user = new Administrator(login, password, email, needNotify);
                    break;
                case Roles.Manager:
                    user = new Manager(login, password);
                    break;
                case Roles.SecurityGuard:
                    user = new SecurityGuard(login, password);
                    break;

                default: throw new NotImplementedException($"Unexpected role: {Enum.GetName(typeof(Roles), role)}");
            }

            _userRepository.Add(user);

            return GetByLogin(login);
        }

        public User GetById(int id)
        {
            return _userRepository.FindById(id);
        }

        public User GetByLogin(string login)
        {
            return _userRepository.All().SingleOrDefault(x => x.Login.Equals(login));
        }

        public IEnumerable<User> GetAllActive()
        {
            return _userRepository.AllActive();
        }
    }
}