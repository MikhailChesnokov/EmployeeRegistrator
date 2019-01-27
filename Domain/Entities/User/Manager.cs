namespace Domain.Entities.User
{
    using System;



    public sealed class Manager : User
    {
        [Obsolete("Only for reflection", true)]
        public Manager() { }

        public Manager(
            string login,
            string password)
            : base(
                login,
                password,
                Role.Manager)
        {

        }
    }
}