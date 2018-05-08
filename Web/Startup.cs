namespace Web
{
    using System;
    using Application.Infrastructure.Filters;
    using Autofac;
    using global::Autofac;
    using global::Autofac.Extensions.DependencyInjection;
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

        public IContainer Container { get; set; }



        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services
                .AddScoped<ExceptionFilter>();

            services
                .AddMvc(options => options.Filters.AddService<ExceptionFilter>())
                .AddTypedRouting();

            services
                .AddAuthentication("CookieScheme")
                .AddCookie("CookieScheme", options =>
                {
                    options.Cookie.Name = "EmployeeRegistratorCookie";
                    options.LoginPath = new PathString("/Account/SignIn");
                });

            ContainerBuilder containerBuilder = new ContainerBuilder();
            containerBuilder.Populate(services);
            containerBuilder.RegisterConfiguredModulesFromAssemblyContaining<ServiceModule>(Configuration as IConfigurationRoot);
            Container = containerBuilder.Build();

            return new AutofacServiceProvider(Container);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime lifetime)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes => routes.MapRoute("default", "{controller=Employee}/{action=List}/{id?}"));

            lifetime.ApplicationStopping.Register(() => Container.Dispose());
        }
    }
}