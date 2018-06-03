namespace Domain.Services.User
{
    using Entities.User;



    public interface IUserService : IDomainService
    {
        void SignUp(string login, string password);

        User GetById(int id);
    }
}