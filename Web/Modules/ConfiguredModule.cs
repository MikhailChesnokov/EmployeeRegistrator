namespace Web.Modules
{
    using Autofac;
    using Microsoft.Extensions.Configuration;



    public abstract class ConfiguredModule : Module
    {
        public IConfigurationRoot ConfigurationRoot { get; set; }
    }
}