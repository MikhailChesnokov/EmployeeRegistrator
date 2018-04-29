namespace Web.Application.Controllers
{
    public interface IFormHandlerFactory
    {
        IFormHandler<TForm> Create<TForm>()
            where TForm : IForm;

        IFormHandler<TForm, TFormResult> Create<TForm, TFormResult>()
            where TForm : IForm;
    }
}