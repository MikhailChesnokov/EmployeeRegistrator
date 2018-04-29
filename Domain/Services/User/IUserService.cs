namespace Domain.Services.User
{
    public interface IUserService : IDomainService
    {
        void SignUp(string login, string password);
    }
}