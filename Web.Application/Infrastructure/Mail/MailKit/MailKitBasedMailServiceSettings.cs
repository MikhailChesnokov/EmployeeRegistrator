namespace Web.Application.Infrastructure.Mail.MailKit
{
    public class MailKitBasedMailServiceSettings
    {
        public string Host { get; set; }
        
        public ushort Port { get; set; }
        
        public bool UseSsl { get; set; }
        
        public bool NeedAuthentication { get; set; }
        
        public string Login { get; set; }
        
        public string Password { get; set; }
        
        public string From { get; set; }
    }
}