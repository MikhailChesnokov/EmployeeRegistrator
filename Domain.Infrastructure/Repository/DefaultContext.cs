namespace Domain.Infrastructure.Repository
{
    using System;
    using System.IO;
    using Components.Password;
    using Entities.Employee;
    using Entities.Registration;
    using Entities.User;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;



    public sealed class DefaultContext : DbContext
    {
        public DefaultContext()
        {
            SetConfigurationRoot();

            Database.EnsureCreated();
        }



        private IConfigurationRoot ConfigurationRoot { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Registration> Registrations { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Password> Passwords { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            switch (ConfigurationRoot["Database"].ToLower())
            {
                case "sqlserver":
                    optionsBuilder.UseSqlServer(ConfigurationRoot.GetConnectionString("SqlServer"));
                    break;

                case "mysql":
                    optionsBuilder.UseMySQL(ConfigurationRoot.GetConnectionString("MySql"));
                    break;

                default:
                    throw new InvalidOperationException("Unexpected \"Database\" parameter in appsettings.json. Try \"SqlServer\" or \"MySql\".");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasOne(x => x.Password);

            switch (ConfigurationRoot["Database"].ToLower())
            {
                case "sqlserver":
                    modelBuilder.Entity<Password>().Property(x => x.Salt).IsRequired().HasColumnType("varbinary(40)").HasMaxLength(40);
                    modelBuilder.Entity<Password>().Property(x => x.Hash).IsRequired().HasColumnType("varbinary(256)").HasMaxLength(256);
                    break;

                case "mysql":
                    modelBuilder.Entity<Password>().Property(x => x.Salt).IsRequired().HasColumnType("MEDIUMBLOB").HasMaxLength(40);
                    modelBuilder.Entity<Password>().Property(x => x.Hash).IsRequired().HasColumnType("MEDIUMBLOB").HasMaxLength(256);
                    break;

                default:
                    throw new InvalidOperationException("Unexpected \"Database\" parameter in appsettings.json. Try \"SqlServer\" or \"MySql\".");
            }

            base.OnModelCreating(modelBuilder);
        }

        private void SetConfigurationRoot()
        {
            ConfigurationBuilder configBuilder = new ConfigurationBuilder();
            configBuilder.SetBasePath(Directory.GetCurrentDirectory());
            configBuilder.AddJsonFile("appsettings.json");
            IConfigurationRoot config = configBuilder.Build();

            ConfigurationRoot = config;
        }
    }
}