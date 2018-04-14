namespace Web.Application.Controllers
{
    using System;
    using Domain.Repository;
    using Employee.Forms;
    using Employee.Forms.Handlers;



    public class FormHandlerFactory : IFormHandlerFactory
    {
        private readonly IRepository<Domain.Entities.Employee> _employeeRepository;



        public FormHandlerFactory(
            IRepository<Domain.Entities.Employee> employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }



        public IFormHandler<TForm> Create<TForm>()
            where TForm : IForm
        {
            if (typeof(TForm) == typeof(EditEmployeeForm))
                return new EditEmployeeFormHandler(_employeeRepository) as IFormHandler<TForm>;
            if (typeof(TForm) == typeof(DeleteEmployeeForm))
                return new DeleteEmployeeFormHandler(_employeeRepository) as IFormHandler<TForm>;

            throw new InvalidOperationException("Undefined type.");
        }

        public IFormHandler<TForm, TFormResult> Create<TForm, TFormResult>()
            where TForm : IForm
        {
            if (typeof(TForm) == typeof(CreateEmployeeForm))
                return new CreateEmployeeFormHandler(_employeeRepository) as IFormHandler<TForm, TFormResult>;
            

            throw new InvalidOperationException("Undefined type");
        }
    }
}