namespace Web.Application.Infrastructure.Mail
{
    public interface IMailService
    {
        void Send(string addressee, MailMessage mailMessage);
    }
}