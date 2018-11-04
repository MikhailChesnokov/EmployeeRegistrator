namespace Domain.Infrastructure.Extensions
{
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Reflection;
    using Entities.Registration;



    public static class EnumExtensions
    {
        public static string DisplayName(this RegistrationEventType eventType)
        {
            return
                typeof(RegistrationEventType)
                    .GetFields()
                    .FirstOrDefault(x => x.Name == eventType.ToString())
                    ?.GetCustomAttributes<DisplayAttribute>()
                    ?.FirstOrDefault()
                    ?.Name;
        }
    }
}