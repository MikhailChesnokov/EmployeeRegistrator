namespace Web.Application.Controllers.EmployeeRegistration
{
    using Domain.Entities;
    using Domain.Repository;
    using Domain.Services;
    using Forms;
    using Microsoft.AspNetCore.Mvc;



    public class EmployeeRegistrationController : Controller
    {
        private readonly IRegistrationService _registrationService;

        private readonly IRepository<Employee> _employeeRepository;



        public EmployeeRegistrationController(
            IRegistrationService registrationService,
            IRepository<Employee> employeeRepository)
        {
            _registrationService = registrationService;
            _employeeRepository = employeeRepository;
        }



        [HttpPost]
        public void RegisterComing([FromBody] RegisterEventForm form)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    new RegisterEventFormHandler(_registrationService, _employeeRepository).Execute(form);
                }
                catch (FormException e)
                {
                    
                }
            }
        }

        [HttpPost]
        public void RegisterLeaving([FromBody] RegisterEventForm form)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    new RegisterEventFormHandler(_registrationService, _employeeRepository).Execute(form);
                }
                catch (FormException e)
                {

                }
            }
        }
    }
}