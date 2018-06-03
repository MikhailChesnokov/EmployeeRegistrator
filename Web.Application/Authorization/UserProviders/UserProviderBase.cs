namespace Web.Application.Authorization.UserProviders
{
    public abstract class UserProviderBase<TUser> : IUserProvider<TUser>
    {
        public TUser User => GetUser();



        protected abstract TUser GetUser();
    }
}