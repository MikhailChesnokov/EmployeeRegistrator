namespace Web.Application.Infrastructure.Mail.MailKit
{
    using global::MailKit.Net.Smtp;
    using Mail;
    using MimeKit;



    public sealed class MailKitBasedMailService : IMailService
    {
        private readonly MailKitBasedMailServiceSettings _basedMailServiceSettings;
        
        
        
        public MailKitBasedMailService(MailKitBasedMailServiceSettings settings)
        {
            _basedMailServiceSettings = settings;
        }


        
        public void Send(string addressee, MailMessage mailMessage)
        {
            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = mailMessage.Body
            };

            var message = new MimeMessage
            {
                Subject = mailMessage.Subject,
                Body = bodyBuilder.ToMessageBody()
            };
            
            message.From.Add(new MailboxAddress(_basedMailServiceSettings.From));
            message.To.Add(new MailboxAddress(addressee));

            using (var smtpClient = new SmtpClient())
            {
                smtpClient.ServerCertificateValidationCallback = (s, c, h, e) => true;

                smtpClient.Connect(_basedMailServiceSettings.Host, _basedMailServiceSettings.Port, _basedMailServiceSettings.UseSsl);

                if (_basedMailServiceSettings.NeedAuthentication)
                    smtpClient.Authenticate(_basedMailServiceSettings.Login, _basedMailServiceSettings.Password);

                smtpClient.Send(message);

                smtpClient.Disconnect(true);
            }
        }
    }
}