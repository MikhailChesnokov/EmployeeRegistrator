namespace Domain.Entities.Employee.Exceptions
{
    using System;



    public class EmployeeAlreadyRemovedException : Exception
    {
        public EmployeeAlreadyRemovedException(string message) : base(message) { }
    }
}