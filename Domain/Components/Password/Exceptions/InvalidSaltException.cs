namespace Domain.Components.Password.Exceptions
{
    using System;



    public class InvalidSaltException : Exception
    {
        public InvalidSaltException(string message) : base(message) { }
    }
}