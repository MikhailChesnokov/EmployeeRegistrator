namespace Web
{
    using Application.Infrastructure.Filters;
    using Application.Infrastructure.ScheduledTasks;
    using Extensions;
    using Autofac;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Modules;



    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }



        public IConfiguration Configuration { get; set; }



        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddScoped<ExceptionFilter>()
                .AddHostedService<TimedHostedService>();

            services
                .AddMvc(options => options.Filters.AddService<ExceptionFilter>())
                .AddTypedRouting();

            services
                .AddAuthentication("CookieScheme")
                .AddCookie("CookieScheme", options =>
                {
                    options.Cookie.Name = "EmployeeRegistratorCookie";
                    options.AccessDeniedPath = new PathString("/Error/AccessDenied");
                    options.LoginPath = new PathString("/Account/SignIn");
                    options.LogoutPath = new PathString("/Account/SignOut");
                    options.ReturnUrlParameter = "returnUrl";
                });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterConfiguredModulesFromAssemblyContaining<RepositoryModule>(Configuration as IConfigurationRoot);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStatusCodePagesWithRedirects("/Error/Code/{0}");

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseRequestLocalization("ru");

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    "error",
                    "Error/{action=Index}/{code?}",
                    new { controller = "Error" });

                routes.MapRoute(
                    "default",
                    "{controller=Employee}/{action=List}/{id?}");
            });
        }
    }
}