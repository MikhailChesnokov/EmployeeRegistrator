namespace Domain.Entities.User
{
    using System;
    using Entrance;


    public class SecurityGuard : User
    {
        [Obsolete("Only for reflection", true)]
        public SecurityGuard() { }

        public SecurityGuard(
            string login,
            string password,
            Entrance entrance)
            : base(
                login,
                password,
                Role.SecurityGuard)
        {
            ChangeEntrance(entrance);
        }


        
        public Entrance Entrance { get; protected set; }


        
        protected internal void ChangeEntrance(Entrance entrance)
        {
            Entrance = entrance ?? throw new ArgumentNullException(nameof(entrance));
        }
    }
}