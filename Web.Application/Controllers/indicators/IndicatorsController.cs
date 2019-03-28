namespace Web.Application.Controllers.indicators
{
    using System.Linq;
    using Authorization.UserProviders;
    using AutoMapper;
    using Domain.Entities.Registration;
    using Domain.Entities.User;
    using Domain.Services.Employee;
    using Domain.Services.Registration;
    using Domain.Services.Time;
    using Domain.Services.User;
    using Employee.ViewModels;
    using Forms;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Registration.Filters;
    using ViewModels;


    [Authorize]
    public class IndicatorsController : FormControllerBase
    {
        private readonly IRegistrationService _registrationService;
        private readonly IUserProvider<User> _userProvider;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly ITimeService _timeService;
        private readonly IEmployeeService _employeeService;
        
        
        
        public IndicatorsController(
            IFormHandlerFactory formHandlerFactory,
            IAuthorizationService authorizationService,
            IRegistrationService registrationService,
            IUserProvider<User> userProvider,
            IUserService userService,
            IMapper mapper,
            ITimeService timeService,
            IEmployeeService employeeService)
            : base(
                formHandlerFactory,
                authorizationService)
        {
            _registrationService = registrationService;
            _userProvider = userProvider;
            _userService = userService;
            _mapper = mapper;
            _timeService = timeService;
            _employeeService = employeeService;
        }



        [HttpGet]
        public IActionResult List()
        {
            if (!RoleIs(Role.Manager, Role.SecurityGuard))
                return Forbid();

            var registrations =
                _registrationService
                    .AllInclude(x => x.Employee, x => x.Entrance.Building)
                    .ForPeriod(_timeService.Now, _timeService.Now)
                    .ToList();

            if (RoleIs(Role.Manager))
            {
                var user = _userProvider.User;

                var manager = _userService.GetById(user.Id) as Manager;

                registrations = registrations.Where(x => _employeeService.GetById(x.Employee.Id).Department.Id == manager.Department.Id).ToList();
            }
            else if (RoleIs(Role.SecurityGuard))
            {
                var user = _userProvider.User;

                var securityGuard = _userService.GetById(user.Id) as SecurityGuard;

                registrations = registrations.Where(x => x.Entrance.Building.Id == securityGuard.Entrance.Building.Id).ToList();
            }
            
            var form = new IndicatorsListForm
            {
                DateFrom = _timeService.Now,
                DateTo = _timeService.Now,
                DayIndicators = registrations.GroupBy(x => x.DateTime.Date).Select(x => new DayIndicatorViewModel
                {
                    DateTime = x.Key,
                    TotalCameCount = registrations.Where(y => y.EventType == RegistrationEventType.Coming).GroupBy(y => y.Employee).Count(),
                    TotalGoneCount = registrations.Where(y => y.EventType == RegistrationEventType.Leaving).GroupBy(y => y.Employee).Count(),
                    In = registrations
                        .GroupBy(y => y.Employee)
                        .Where(y => y.OrderBy(z => z.DateTime).Last().EventType == RegistrationEventType.Coming)
                        .Select(y => _mapper.Map<EmployeeViewModel>(y.Key))
                })
            };
            
            return View(form);
        }
        
        [HttpPost]
        public IActionResult List(IndicatorsListForm form)
        {
            if (!RoleIs(Role.Manager, Role.SecurityGuard))
                return Forbid();

            var registrations =
                _registrationService
                    .AllInclude(x => x.Employee, x => x.Entrance.Building)
                    .ForPeriod(form.DateFrom, form.DateTo)
                    .ToList();
            
            if (RoleIs(Role.Manager))
            {
                var user = _userProvider.User;

                var manager = _userService.GetById(user.Id) as Manager;

                registrations = registrations.Where(x => _employeeService.GetById(x.Employee.Id).Department.Id == manager.Department.Id).ToList();
            }
            else if (RoleIs(Role.SecurityGuard))
            {
                var user = _userProvider.User;

                var securityGuard = _userService.GetById(user.Id) as SecurityGuard;

                registrations = registrations.Where(x => x.Entrance.Building.Id == securityGuard.Entrance.Building.Id).ToList();
            }
            
            var viewModel = new IndicatorsListForm
            {
                DateFrom = form.DateFrom,
                DateTo = form.DateTo,
                DayIndicators = registrations.GroupBy(x => x.DateTime.Date).Select(x => new DayIndicatorViewModel
                {
                    DateTime = x.Key,
                    TotalCameCount = registrations.Where(y => y.EventType == RegistrationEventType.Coming).GroupBy(y => y.Employee).Count(),
                    TotalGoneCount = registrations.Where(y => y.EventType == RegistrationEventType.Leaving).GroupBy(y => y.Employee).Count(),
                    In = registrations
                        .GroupBy(y => y.Employee)
                        .Where(y => y.OrderBy(z => z.DateTime).Last().EventType == RegistrationEventType.Coming)
                        .Select(y => _mapper.Map<EmployeeViewModel>(y.Key))
                })
            };
            
            return View(viewModel);
        }
    }
}