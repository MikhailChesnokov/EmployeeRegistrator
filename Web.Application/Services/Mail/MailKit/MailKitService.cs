namespace Web.Application.Services.Mail.MailKit
{
    using global::MailKit.Net.Smtp;
    using MimeKit;



    public sealed class MailKitService : IMailService
    {
        private readonly MailKitSettings _settings;
        
        
        
        public MailKitService(MailKitSettings settings)
        {
            _settings = settings;
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
            
            message.From.Add(new MailboxAddress(_settings.From));
            message.To.Add(new MailboxAddress(addressee));

            using (var smtpClient = new SmtpClient())
            {
                smtpClient.ServerCertificateValidationCallback = (s, c, h, e) => true;

                smtpClient.Connect(_settings.Host, _settings.Port, _settings.UseSsl);

                if (_settings.NeedAuthentication)
                    smtpClient.Authenticate(_settings.Login, _settings.Password);

                smtpClient.Send(message);

                smtpClient.Disconnect(true);
            }
        }
    }
}