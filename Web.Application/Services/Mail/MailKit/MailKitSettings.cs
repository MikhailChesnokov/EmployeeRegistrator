namespace Web.Application.Services.Mail.MailKit
{
    public class MailKitSettings
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