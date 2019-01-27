namespace Domain.Entities.User
{
    using System;



    public sealed class Administrator : User
    {
        [Obsolete("Only for reflection", true)]
        public Administrator() { }

        public Administrator(
            string login,
            string password,
            string email = null,
            bool needNotify = false)
            : base(
                login,
                password,
                Entities.User.Role.Administrator,
                email,
                needNotify)
        {

        }
    }
}