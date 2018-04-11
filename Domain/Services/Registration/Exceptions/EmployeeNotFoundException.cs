namespace Domain.Services.Registration.Exceptions
{
    using System;



    public class EmployeeNotFoundException : Exception
    {
        public EmployeeNotFoundException(string message) : base(message) { }
    }
}