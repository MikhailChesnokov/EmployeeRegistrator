namespace Web.Application.Controllers
{
    using System;
    using Account.Forms;
    using Account.Forms.Handlers;
    using Domain.Infrastructure.Authentication;
    using Domain.Repository;
    using Domain.Services.Employee;
    using Domain.Services.Registration;
    using Domain.Services.User;
    using Employee.Forms;
    using Employee.Forms.Handlers;
    using Registration.Forms;
    using Registration.Forms.Handlers;
    using User.Forms;
    using User.Forms.Handlers;



    public class FormHandlerFactory : IFormHandlerFactory
    {
        private readonly IAuthenticationService<Domain.Entities.User.User> _authenticationService;

        private readonly IEmployeeService _employeeService;

        private readonly IRegistrationService _registrationService;

        private readonly IRepository<Domain.Entities.User.User> _userRepository;

        private readonly IUserService _userService;



        public FormHandlerFactory(
            IEmployeeService employeeService,
            IRegistrationService registrationService,
            IUserService userService,
            IAuthenticationService<Domain.Entities.User.User> authenticationService,
            IRepository<Domain.Entities.User.User> userRepository)
        {
            _employeeService = employeeService;
            _registrationService = registrationService;
            _userService = userService;
            _authenticationService = authenticationService;
            _userRepository = userRepository;
        }



        public IFormHandler<TForm> Create<TForm>()
            where TForm : IForm
        {
            if (typeof(TForm) == typeof(EditEmployeeForm))
                return new EditEmployeeFormHandler(_employeeService) as IFormHandler<TForm>;
            if (typeof(TForm) == typeof(DeleteEmployeeForm))
                return new DeleteEmployeeFormHandler(_employeeService) as IFormHandler<TForm>;

            if (typeof(TForm) == typeof(RegisterComingForm))
                return new RegisterComingFormHandler(_registrationService, _employeeService) as IFormHandler<TForm>;
            if (typeof(TForm) == typeof(RegisterLeavingForm))
                return new RegisterLeavingFormHandler(_registrationService, _employeeService) as IFormHandler<TForm>;

            if (typeof(TForm) == typeof(SignInForm))
                return new SignInFormHandler(_authenticationService, _userRepository) as IFormHandler<TForm>;
            if (typeof(TForm) == typeof(SignUpForm))
                return new SignUpFormHandler(_userService) as IFormHandler<TForm>;

            if (typeof(TForm) == typeof(CreateUserForm))
                return new CreateUserFormHandler(_userService) as IFormHandler<TForm>;

            throw new InvalidOperationException("Undefined type");
        }

        public IFormHandler<TForm, TFormResult> Create<TForm, TFormResult>()
            where TForm : IForm
        {
            if (typeof(TForm) == typeof(CreateEmployeeForm))
                return new CreateEmployeeFormHandler(_employeeService) as IFormHandler<TForm, TFormResult>;


            throw new InvalidOperationException("Undefined type");
        }
    }
}