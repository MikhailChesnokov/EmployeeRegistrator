namespace Domain.Services.User.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Entities.User;
    using Entrance;
    using Exceptions;
    using Repository;



    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<SecurityGuard> _securityGuardRepository;
        private readonly IRepository<Administrator> _administratorRepository;
        private readonly IRepository<Manager> _managerRepository;
        private readonly IEntranceService _entranceService;



        public UserService(
            IRepository<User> userRepository,
            IEntranceService entranceService,
            IRepository<SecurityGuard> securityGuardRepository,
            IRepository<Administrator> administratorRepository,
            IRepository<Manager> managerRepository)
        {
            _userRepository = userRepository;
            _entranceService = entranceService;
            _securityGuardRepository = securityGuardRepository;
            _administratorRepository = administratorRepository;
            _managerRepository = managerRepository;
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

        public User Create(string login, string password, Role role, string email = null, bool needNotify = false, int? entranceId = null)
        {
            if (string.IsNullOrWhiteSpace(login))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(login));
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(password));

            if (_userRepository.AllActive().Any(x => x.Login.Equals(login, StringComparison.InvariantCultureIgnoreCase)))
                throw new UserAlreadyExistsException("Пользователь с таким логином уже существует");

            User user;

            switch (role)
            {
                case Role.Administrator:
                    user = new Administrator(login, password, email, needNotify);
                    break;
                case Role.Manager:
                    user = new Manager(login, password);
                    break;
                case Role.SecurityGuard:
                {
                    if (!entranceId.HasValue)
                        throw new InvalidOperationException("Entrance not chosen.");
                    
                    var entrance = _entranceService.GetById(entranceId.Value);
                    
                    user = new SecurityGuard(login, password, entrance);
                    break;
                }
                default: throw new ArgumentOutOfRangeException(nameof(role));
            }

            _userRepository.Add(user);

            return GetByLogin(login);
        }

        public User GetById(int id)
        {
            var user = _userRepository.FindById(id);

            if (user is SecurityGuard guard)
                return _securityGuardRepository.FindByIdInclude(id, x => x.Entrance.Building);

            return user;
        }

        public User GetByLogin(string login)
        {
            return _userRepository.AllActive().SingleOrDefault(x => x.Login.Equals(login));
        }

        public IEnumerable<User> GetAllActive()
        {
            return _userRepository.AllActive();
        }
    }
}