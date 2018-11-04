namespace Web.Application.Infrastructure.Mail.Fake
{
    using Microsoft.Extensions.Logging;



    public class FakeMailService : IMailService
    {
        private readonly ILogger<FakeMailService> _logger;
        
        
        
        public FakeMailService(ILogger<FakeMailService> logger)
        {
            _logger = logger;
        }

        
        
        public void Send(string addressee, MailMessage mailMessage)
        {
            _logger.LogInformation($"## Message sent to {addressee} with the body {mailMessage.Body}. ##");    
        }
    }
}