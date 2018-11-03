namespace Web.Application.Services.MailNotification
{
    using System;



    public interface IMailNotificationService
    {
        void Notify(TimeSpan workDayStartTime);
    }
}