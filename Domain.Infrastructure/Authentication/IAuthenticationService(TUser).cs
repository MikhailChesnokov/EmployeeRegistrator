namespace Domain.Infrastructure.Authentication
{
    public interface IAuthenticationService<in TUser>
    {
        void SignIn(TUser user);

        void SignOut();
    }
}