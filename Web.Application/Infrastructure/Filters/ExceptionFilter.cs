namespace Web.Application.Infrastructure.Filters
{
    using System.Threading.Tasks;
    using Controllers.Registration.Exceptions;
    using Domain.Services.Registration.Exceptions;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;



    public class ExceptionFilter : IExceptionFilter, IAsyncExceptionFilter
    {
        public async Task OnExceptionAsync(ExceptionContext context)
        {
            OnException(context);
        }

        public void OnException(ExceptionContext context)
        {
            switch (context.Exception)
            {
                case InvalidRequestParameterException _:
                    context.HttpContext.Response.StatusCode = StatusCodes.Status409Conflict;
                    context.Result = new JsonResult(new
                    {
                        Error = context.Exception.Message
                    });
                    context.ExceptionHandled = true;
                    break;

                case EmployeeNotFoundException _:
                    context.HttpContext.Response.StatusCode = StatusCodes.Status409Conflict;
                    context.Result = new JsonResult(new
                    {
                        Error = context.Exception.Message
                    });
                    context.ExceptionHandled = true;
                    break;
            }
        }
    }
}