namespace Domain.Entities.User
{
    using System;



    public class SecurityGuard : User
    {
        [Obsolete("Only for reflection", true)]
        public SecurityGuard() { }

        public SecurityGuard(
            string login,
            string password)
            : base(
                login,
                password,
                Roles.SecurityGuard)
        {

        }
    }
}