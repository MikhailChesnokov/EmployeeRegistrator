namespace Domain.Services.User.Exceptions
{
    using System;



    public class UserAlreadyExistsException : Exception
    {
        public UserAlreadyExistsException(string message) : base(message) { }
    }
}