namespace Web.Application.Controllers.Registration
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using Domain.Entities;
    using Domain.Repository;
    using Domain.Services.Registration;
    using Exceptions;
    using Forms;
    using Forms.Handlers;
    using Microsoft.AspNetCore.Mvc;
    using ViewModels;



    public class RegistrationController : FormControllerBase
    {
        private readonly IRepository<Employee> _employeeRepository;

        private readonly IMapper _mapper;

        private readonly IRepository<Registration> _registrationRepository;

        private readonly IRegistrationService _registrationService;



        public RegistrationController(
            IFormHandlerFactory formHandlerFactory,
            IRegistrationService registrationService,
            IRepository<Employee> employeeRepository,
            IRepository<Registration> registrationRepository,
            IMapper mapper) : base(formHandlerFactory)
        {
            _registrationService = registrationService;
            _employeeRepository = employeeRepository;
            _registrationRepository = registrationRepository;
            _mapper = mapper;
        }



        [HttpPost]
        public void RegisterComing([FromBody] RegisterComingForm form)
        {
            if (!ModelState.IsValid)
                throw new InvalidRequestParameterException("Invalid request parameters.");

            new RegisterComingFormHandler(_registrationService).Execute(form);
        }

        [HttpPost]
        public void RegisterLeaving([FromBody] RegisterComingForm form)
        {
            if (!ModelState.IsValid)
                throw new InvalidRequestParameterException("Invalid request parameters.");

            new RegisterComingFormHandler(_registrationService).Execute(form);
        }


        [HttpGet]
        public IActionResult List()
        {
            IEnumerable<Registration> registrations = _registrationRepository.All();

            IEnumerable<Employee> employees = _employeeRepository.All();

            IEnumerable<EmployeeRegistrationViewModel> registrationsViewModels = _mapper.Map<IEnumerable<EmployeeRegistrationViewModel>>(registrations);

            registrationsViewModels.ToList().ForEach(x => x.EmployeeFio = employees.Single(y => y.Id == x.EmployeeId).Fio);

            return View(registrationsViewModels);
        }
    }
}