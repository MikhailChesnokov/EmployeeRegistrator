namespace Web.Application.Controllers
{
    public interface IFormHandler<in TForm>
        where TForm : IForm
    {
        void Execute(TForm form);
    }



    public interface IFormHandler<in TForm, out TFormResult>
        where TForm : IForm
    {
        TFormResult Execute(TForm form);
    }
}