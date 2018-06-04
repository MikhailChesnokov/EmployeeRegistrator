namespace Domain.Services.User
{
    using System.Collections.Generic;
    using Entities.User;



    public interface IUserService : IDomainService
    {
        void SignUp(string login, string password);

        User Create(string login, string password, Roles role);

        User GetById(int id);

        User GetByLogin(string login);

        IEnumerable<User> GetAllActive();
    }
}