namespace Web.Application.Infrastructure.Extensions
{
    using System.Collections.Generic;
    using System.Linq;
    using Domain.Entities.Employee;
    using Microsoft.AspNetCore.Mvc.Rendering;



    public static class EnumerableExtensions
    {
        public static IEnumerable<SelectListItem> ToSelectList(this IEnumerable<Employee> employees)
        {
            return
                employees
                    .Select(x => new SelectListItem
                    {
                        Text = x.Fio,
                        Value = x.Id.ToString(),
                        Disabled = false,
                        Group = default,
                        Selected = false
                    });
        }
    }
}