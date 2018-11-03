namespace Web.Application.Controllers
{
    using System;



    internal sealed class FormException : Exception
    {
        public FormException(string message) : base(message) { }
    }
}