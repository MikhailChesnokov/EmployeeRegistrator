namespace Domain.Exceptions
{
    using System;



    public class CannotDeleteEntityInUseException : Exception
    {
        public CannotDeleteEntityInUseException(string message) : base(message) { }
    }
}