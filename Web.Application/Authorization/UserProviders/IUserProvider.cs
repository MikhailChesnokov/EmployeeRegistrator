namespace Web.Application.Authorization.UserProviders
{
    public interface IUserProvider<out TUser>
    {
        TUser User { get; }
    }
}