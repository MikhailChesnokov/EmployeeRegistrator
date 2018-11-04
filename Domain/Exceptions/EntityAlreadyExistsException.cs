namespace Domain.Exceptions
{
    using System;



    public class EntityAlreadyExistsException : Exception
    {
        public EntityAlreadyExistsException(string message) : base(message) { }
    }
}