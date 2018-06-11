namespace Web.Application.Controllers.Registration.Enums
{
    using System;


    [AttributeUsage(AttributeTargets.Field)]
    public class LatenessTimeAttribute : Attribute
    {
        public LatenessTimeAttribute(int hours , int minutes)
        {
            LatenessTimeSpan = TimeSpan.FromMinutes(hours * 60 + minutes);
        }



        public TimeSpan LatenessTimeSpan { get; }
    }
}