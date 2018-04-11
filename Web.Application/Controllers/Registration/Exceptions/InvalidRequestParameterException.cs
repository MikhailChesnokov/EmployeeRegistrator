namespace Web.Application.Controllers.Registration.Exceptions
{
    using System;



    public class InvalidRequestParameterException : Exception
    {
        public InvalidRequestParameterException(string message) : base(message) { }
    }
}