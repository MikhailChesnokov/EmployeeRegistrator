namespace Domain.Services.User
{
    using System.Collections.Generic;
    using Entities.User;



    public interface IUserService : IDomainService
    {
        void SignUp(string login, string password);

        User Create(
            string login,
            string password,
            Role role,
            string email = null,
            bool needNotify = false,
            int? entranceId = null,
            int? departmentId = null);

        User GetById(int id);

        User GetByLogin(string login);

        void ChangeLogin(User user, string login);

        IEnumerable<User> GetAllActive();

        void Delete(User user);
    }
}