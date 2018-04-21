namespace Domain.Entities.User
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Components.Password;



    public class User : IEntity
    {
        [Obsolete("Only for reflection", true)]
        public User() { }

        [SuppressMessage("ReSharper", "VirtualMemberCallInConstructor")]
        public User(string login, string password)
        {
            SetLogin(login);
            SetPassword(password);
        }



        public virtual string Login { get; protected set; }

        public virtual Password Password { get; protected set; }

        public virtual int Id { get; set; }



        protected virtual void SetLogin(string login)
        {
            if (string.IsNullOrWhiteSpace(login))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(login));

            Login = login.Trim();
        }

        protected virtual void SetPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(password));

            Password = new Password(password);
        }



        public virtual bool CheckPassword(string password)
        {
            return Password.CheckPassword(password);
        }
    }
}