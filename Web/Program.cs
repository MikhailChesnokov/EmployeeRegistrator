namespace Web
{
    using global::Autofac.Extensions.DependencyInjection;
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;



    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                          .CaptureStartupErrors(true)
                          .UseSetting(WebHostDefaults.DetailedErrorsKey, "true")
                          .UseStartup<Startup>()
                          .ConfigureServices(services => services.AddAutofac())
                          .Build();
        }
    }
}