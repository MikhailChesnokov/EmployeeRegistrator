namespace Domain.Entities.User
{
    using System;
    using Components.Password;



    public abstract class User : IEntity
    {
        [Obsolete("Only for reflection", true)]
        public User() { }



        protected User(
            string login,
            string password,
            Role role,
            string email = null,
            bool needNotify = false)
        {
            ChangeLogin(login);
            SetPassword(password);
            ChangeRole(role);
            ChangeNotification(email, needNotify);
        }



        public string Login { get; protected set; }

        public Password Password { get; protected set; }

        

        public Role Role { get; protected set; }
        
        public string Email { get; protected set; }

        public bool NeedNotify { get; protected set; }

        public int Id { get; set; }



        protected internal void ChangeLogin(string login)
        {
            if (string.IsNullOrWhiteSpace(login))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(login));

            Login = login.Trim();
        }

        private void SetPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(password));

            Password = new Password(password);
        }

        public void ChangeRole(Role role)
        {
            Role = role;
        }

        public void ChangeNotification(string email, bool needNotify)
        {
            if (string.IsNullOrWhiteSpace(email) && needNotify)
                throw new ArgumentException("Empty email when need notification.");

            Email = email;
            NeedNotify = needNotify;
        }


        public bool CheckPassword(string password)
        {
            return Password.CheckPassword(password);
        }
    }
}