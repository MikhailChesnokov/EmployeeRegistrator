﻿namespace Domain.Entities.User
{
    using System;
    using Components.Password;



    public class User : IEntity
    {
        [Obsolete("Only for reflection", true)]
        public User() { }



        protected User(string login, string password, Roles role)
        {
            SetLogin(login);
            SetPassword(password);
            SetRole(role);
        }



        public string Login { get; protected set; }

        public Password Password { get; protected set; }

        public Roles Role { get; protected set; }

        public int Id { get; set; }



        private void SetLogin(string login)
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

        private void SetRole(Roles role)
        {
            Role = role;
        }


        public bool CheckPassword(string password)
        {
            return Password.CheckPassword(password);
        }
    }
}