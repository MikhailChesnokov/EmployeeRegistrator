namespace Domain.Components.Password.Exceptions
{
    using System;



    public class TooShortPasswordException : Exception
    {
        public TooShortPasswordException(string message) : base(message) { }
    }
}