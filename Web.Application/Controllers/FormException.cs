namespace Web.Application.Controllers
{
    using System;



    public class FormException : Exception
    {
        public FormException(string message) : base(message) { }
    }
}