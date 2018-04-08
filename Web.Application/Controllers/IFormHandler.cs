namespace Web.Application.Controllers
{
    public interface IFormHandler<in TForm>
        where TForm : IForm
    {
        void Execute(TForm form);
    }



    public interface IFormHandler<in TForm, out TResult>
        where TForm : IForm
    {
        TResult Execute(TForm form);
    }
}