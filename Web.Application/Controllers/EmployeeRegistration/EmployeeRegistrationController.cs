namespace Web.Application.Controllers.EmployeeRegistration
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using Domain.Entities;
    using Domain.Repository;
    using Domain.Services;
    using Forms;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ActionConstraints;
    using ViewModels;



    public class EmployeeRegistrationController : Controller
    {
        private readonly IRegistrationService _registrationService;

        private readonly IRepository<Employee> _employeeRepository;

        private readonly IRepository<EmployeeRegistration> _registrationRepository;

        private readonly IMapper _mapper;



        public EmployeeRegistrationController(
            IRegistrationService registrationService,
            IRepository<Employee> employeeRepository,
            IRepository<EmployeeRegistration> registrationRepository,
            IMapper mapper)
        {
            _registrationService = registrationService;
            _employeeRepository = employeeRepository;
            _registrationRepository = registrationRepository;
            _mapper = mapper;
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


        [HttpGet]
        public IActionResult List()
        {
            IEnumerable<EmployeeRegistration> registrations = _registrationRepository.All();

            IEnumerable<EmployeeRegistrationViewModel> registrationsViewModels = _mapper.Map<IEnumerable<EmployeeRegistrationViewModel>>(registrations);

            return View(registrationsViewModels);
        }
    }
}