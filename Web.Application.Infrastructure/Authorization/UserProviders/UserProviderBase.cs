namespace Web.Application.Authorization.UserProviders
{
    public abstract class UserProviderBase<TUser> : IUserProvider<TUser>
    {
        public TUser User => GetUser();

        public abstract void DropUser();

        protected abstract TUser GetUser();
    }
}