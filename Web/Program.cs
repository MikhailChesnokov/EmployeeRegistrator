namespace Web
{
    using System.IO;
    using global::Autofac.Extensions.DependencyInjection;
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;



    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                         .SetBasePath(Directory.GetCurrentDirectory())
                         .AddJsonFile("hosting.json", true)
                         .AddCommandLine(args)
                         .Build();

            return WebHost.CreateDefaultBuilder(args)
                          .CaptureStartupErrors(true)
                          .UseSetting(WebHostDefaults.DetailedErrorsKey, "true")
                          .UseStartup<Startup>()
                          .UseConfiguration(config)
                          .ConfigureServices(services => services.AddAutofac())
                          .Build();
        }
    }
}