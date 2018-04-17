namespace Web.Application.Controllers
{
    using System;
    using Domain.Services.Employee;
    using Domain.Services.Registration;
    using Employee.Forms;
    using Employee.Forms.Handlers;
    using Registration.Forms;
    using Registration.Forms.Handlers;



    public class FormHandlerFactory : IFormHandlerFactory
    {
        private readonly IEmployeeService _employeeService;

        private readonly IRegistrationService _registrationService;



        public FormHandlerFactory(
            IEmployeeService employeeService,
            IRegistrationService registrationService)
        {
            _employeeService = employeeService;
            _registrationService = registrationService;
        }



        public IFormHandler<TForm> Create<TForm>()
            where TForm : IForm
        {
            if (typeof(TForm) == typeof(EditEmployeeForm))
                return new EditEmployeeFormHandler(_employeeService) as IFormHandler<TForm>;
            if (typeof(TForm) == typeof(DeleteEmployeeForm))
                return new DeleteEmployeeFormHandler(_employeeService) as IFormHandler<TForm>;

            if (typeof(TForm) == typeof(RegisterComingForm))
                return new RegisterComingFormHandler(_registrationService) as IFormHandler<TForm>;
            if (typeof(TForm) == typeof(RegisterLeavingForm))
                return new RegisterLeavingFormHandler(_registrationService) as IFormHandler<TForm>;



            throw new InvalidOperationException("Undefined type.");
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