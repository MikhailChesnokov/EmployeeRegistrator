namespace Web.Application.Services.MailNotification
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Domain.Entities.Employee;
    using Domain.Entities.Registration;
    using Domain.Services.Employee;
    using Domain.Services.Registration;
    using Domain.Services.User;
    using Infrastructure.Mail;



    public class MailNotificationService : IMailNotificationService
    {
        private readonly IEmployeeService _employeeService;
        private readonly IRegistrationService _registrationService;
        private readonly IUserService _userService;
        private readonly IMailService _mailService;


        
        public MailNotificationService(
            IRegistrationService registrationService,
            IEmployeeService employeeService,
            IUserService userService,
            IMailService mailService)
        {
            _registrationService = registrationService;
            _employeeService = employeeService;
            _userService = userService;
            _mailService = mailService;
        }


        
        public void Notify(TimeSpan workDayStartTime)
        {
            var employeesWithLateness = new List<Employee>();
            var missingEmployees = new List<Employee>();

            var lastRegistrationsByEmployee =
                _registrationService
                    .AllInclude(x => x.Employee)
                    .GroupBy(x => x.Employee)
                    .Select(x => new
                    {
                        Employee = x.Key,
                        LastRegistration = x.Last()
                    })
                    .ToList();

            _employeeService.AllActive().ToList().ForEach(x =>
            {
                var employeeWithLastRegistration = lastRegistrationsByEmployee.SingleOrDefault(y => y.Employee.Id == x.Id);
                
                if (employeeWithLastRegistration is null)
                {
                    missingEmployees.Add(x);
                }
                else if (employeeWithLastRegistration.LastRegistration.EventType is RegistrationEventType.Leaving)
                {
                    missingEmployees.Add(x);
                }
                else
                {
                    var comingTime = employeeWithLastRegistration.LastRegistration.DateTime.TimeOfDay;

                    if (comingTime > workDayStartTime)
                    {
                        employeesWithLateness.Add(x);
                    }
                }
            });

            var text = CreateEmailTextBody(missingEmployees, employeesWithLateness);

            var recipientEmails = _userService.GetAllActive().Where(x => x.NeedNotify).Select(x => x.Email);

            foreach (var email in recipientEmails)
            {
                _mailService.Send(email, new MailMessage
                {
                    Subject = "Оповещение о явке сотрудников",
                    Body = text
                });
            }
        }

        private string CreateEmailTextBody(List<Employee> missingEmployees, List<Employee> employeesWithLateness)
        {
            var text = string.Empty;

            if (missingEmployees.Count is 0)
            {
                text += "Все сотрудники находятся на рабочих местах.\n\n<br><br>";
            }
            else
            {
                text += $"На рабочих местах отсутствуют следующие сотрудники:\n\n<br>{ToStringList(missingEmployees)}\n\n<br><br>";
            }

            if (employeesWithLateness.Count is 0)
            {
                text += "Среди пришедших сотрудников нет опоздавших.\n\n<br><br>";
            }
            else
            {
                text += $"Следующие сотрудники пришли на работу с опозданием:\n<br>{ToStringList(employeesWithLateness)}";
            }

            return text;
            
            string ToStringList(IEnumerable<Employee> employees) => string.Join("\n<br>", employees.Select(x => $"{x.Fio} ({x.PersonnelNumber})"));
        }
    }
}