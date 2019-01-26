namespace Web.Application.Controllers
{
    using Domain.Services.Building;
    using Building.Forms;
    using Building.Forms.Handlers;
    using System;
    using Account.Forms;
    using Account.Forms.Handlers;
    using Departments.Forms;
    using Departments.Forms.Handlers;
    using Domain.Infrastructure.Authentication;
    using Domain.Repository;
    using Domain.Services.Department;
    using Domain.Services.Employee;
    using Domain.Services.Registration;
    using Domain.Services.User;
    using Employee.Forms;
    using Employee.Forms.Handlers;
    using Registration.Forms;
    using Registration.Forms.Handlers;
    using User.Forms;
    using User.Forms.Handlers;



    public sealed class HardCodedFormHandlerFactory : IFormHandlerFactory
    {
        private readonly IAuthenticationService<Domain.Entities.User.User> _authenticationService;
        private readonly IEmployeeService _employeeService;
        private readonly IRegistrationService _registrationService;
        private readonly IRepository<Domain.Entities.User.User> _userRepository;
        private readonly IUserService _userService;
        private readonly IDepartmentService _departmentService;
        private readonly IBuildingService _buildingService;



        public HardCodedFormHandlerFactory(
            IEmployeeService employeeService,
            IRegistrationService registrationService,
            IUserService userService,
            IAuthenticationService<Domain.Entities.User.User> authenticationService,
            IRepository<Domain.Entities.User.User> userRepository,
            IDepartmentService departmentService, IBuildingService buildingService)
        {
            _employeeService = employeeService;
            _registrationService = registrationService;
            _userService = userService;
            _authenticationService = authenticationService;
            _userRepository = userRepository;
            _departmentService = departmentService;
            _buildingService = buildingService;
        }



        public IFormHandler<TForm> Create<TForm>()
            where TForm : IForm
        {
            if (typeof(TForm) == typeof(EditEmployeeForm))
                return new EditEmployeeFormHandler(_employeeService, _departmentService) as IFormHandler<TForm>;
            if (typeof(TForm) == typeof(DeleteEmployeeForm))
                return new DeleteEmployeeFormHandler(_employeeService) as IFormHandler<TForm>;
            
            if (typeof(TForm) == typeof(EditDepartmentForm))
                return new EditDepartmentFormHandler(_departmentService) as IFormHandler<TForm>;
            if (typeof(TForm) == typeof(DeleteDepartmentForm))
                return new DeleteDepartmentFormHandler(_departmentService) as IFormHandler<TForm>;

            if (typeof(TForm) == typeof(EditBuildingForm))
                return new EditBuildingFormHandler(_buildingService) as IFormHandler<TForm>;
            if (typeof(TForm) == typeof(DeleteBuildingForm))
                return new DeleteBuildingFormHandler(_buildingService) as IFormHandler<TForm>;
            
            if (typeof(TForm) == typeof(RegisterComingForm))
                return new RegisterComingFormHandler(_registrationService, _employeeService) as IFormHandler<TForm>;
            if (typeof(TForm) == typeof(RegisterLeavingForm))
                return new RegisterLeavingFormHandler(_registrationService, _employeeService) as IFormHandler<TForm>;

            if (typeof(TForm) == typeof(SignInForm))
                return new SignInFormHandler(_authenticationService, _userRepository) as IFormHandler<TForm>;
            if (typeof(TForm) == typeof(SignUpForm))
                return new SignUpFormHandler(_userService) as IFormHandler<TForm>;

            throw new InvalidOperationException("Undefined type");
        }

        public IFormHandler<TForm, TFormResult> Create<TForm, TFormResult>()
            where TForm : IForm
        {
            if (typeof(TForm) == typeof(CreateEmployeeForm))
                return new CreateEmployeeFormHandler(_employeeService, _departmentService) as IFormHandler<TForm, TFormResult>;

            if (typeof(TForm) == typeof(CreateUserForm))
                return new CreateUserFormHandler(_userService) as IFormHandler<TForm, TFormResult>;

            if (typeof(TForm) == typeof(CreateDepartmentForm))
                return new CreateDepartmentFormHandler(_departmentService) as IFormHandler<TForm, TFormResult>;
            
            if (typeof(TForm) == typeof(CreateBuildingForm))
                return new CreateBuildingFormHandler(_buildingService) as IFormHandler<TForm, TFormResult>;

            throw new InvalidOperationException("Undefined type");
        }
    }
}