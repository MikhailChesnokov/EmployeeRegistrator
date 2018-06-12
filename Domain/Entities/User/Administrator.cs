namespace Domain.Entities.User
{
    using System;



    public class Administrator : User
    {
        [Obsolete("Only for reflection", true)]
        public Administrator() { }

        public Administrator(
            string login,
            string password)
            : base(
                login,
                password,
                Roles.Administrator)
        {

        }
    }
}