namespace Web.Application.Services.Mail
{
    public interface IMailService
    {
        void Send(string addressee, MailMessage mailMessage);
    }
}