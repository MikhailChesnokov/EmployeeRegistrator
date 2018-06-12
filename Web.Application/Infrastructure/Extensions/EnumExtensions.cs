namespace Web.Application.Infrastructure.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using Controllers.Registration.Enums;
    using Microsoft.AspNetCore.Mvc.Rendering;



    public static class EnumExtensions
    {
        public static IEnumerable<SelectListItem> ToSelectList(this Type enumType,
            string selected = null)
        {
            return
                Enum
                    .GetNames(enumType)
                    .Select(x => new SelectListItem
                    {
                        Value =
                            Enum
                                .Parse(enumType, x)
                                .ToString(),
                        Text =
                            enumType
                                .GetField(x)
                                .GetCustomAttributes(false)
                                .OfType<DisplayAttribute>()
                                .Single()
                                .Name,
                        Group = default,
                        Selected = selected != null && x == selected,
                        Disabled = false
                    });
        }

        public static TimeSpan GetLatenessTimeSpan(this Lateness lateness)
        {
            return
                typeof(Lateness)
                    .GetField(Enum.GetName(typeof(Lateness), lateness))
                    .GetCustomAttributes(false)
                    .OfType<LatenessTimeAttribute>()
                    .Single()
                    .LatenessTimeSpan;
        }
    }
}