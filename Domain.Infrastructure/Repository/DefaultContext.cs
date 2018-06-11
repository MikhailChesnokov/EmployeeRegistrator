namespace Domain.Infrastructure.Repository
{
    using System;
    using System.IO;
    using Entities.Employee;
    using Entities.Registration;
    using Entities.User;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;



    public sealed class DefaultContext : DbContext
    {
        private readonly ILoggerFactory _loggerFactory;

        public DefaultContext(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
            SetConfigurationRoot();
            
            Database.EnsureCreated();
        }



        private IConfigurationRoot ConfigurationRoot { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Registration> Registrations { get; set; }

        public DbSet<User> Users { get; set; }



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

                case "sqlite":
                    optionsBuilder.UseSqlite(ConfigurationRoot.GetConnectionString("SQLite"));
                    break;

                case "inmemory":
                    optionsBuilder.UseInMemoryDatabase(ConfigurationRoot.GetConnectionString("InMemory"));
                    break;

                default:
                    throw new InvalidOperationException("Unexpected \"Database\" parameter in appsettings.json. Try \"SqlServer\", \"MySql\" or \"SQLite\".");
            }

            optionsBuilder.UseLoggerFactory(_loggerFactory);

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            switch (ConfigurationRoot["Database"].ToLower())
            {
                case "sqlserver":
                    modelBuilder.Entity<User>().OwnsOne(x => x.Password, pw =>
                    {
                        pw.Property(x => x.Salt).IsRequired().HasColumnType("varbinary(40)").HasMaxLength(40);
                        pw.Property(x => x.Hash).IsRequired().HasColumnType("varbinary(256)").HasMaxLength(256);
                    });
                    break;

                case "mysql":
                    modelBuilder.Entity<User>().OwnsOne(x => x.Password, pw =>
                    {
                        pw.Property(x => x.Salt).IsRequired().HasColumnType("MEDIUMBLOB").HasMaxLength(40);
                        pw.Property(x => x.Hash).IsRequired().HasColumnType("MEDIUMBLOB").HasMaxLength(256);
                    });
                    break;

                case "sqlite":
                    modelBuilder.Entity<User>().OwnsOne(x => x.Password, pw =>
                    {
                        pw.Property(x => x.Salt).IsRequired().HasColumnType("BLOB").HasMaxLength(40);
                        pw.Property(x => x.Hash).IsRequired().HasColumnType("BLOB").HasMaxLength(256);
                    });
                    break;

                case "inmemory":
                    modelBuilder.Entity<User>().OwnsOne(x => x.Password, pw =>
                    {
                        pw.Property(x => x.Salt).IsRequired().HasColumnType("varbinary(40)").HasMaxLength(40);
                        pw.Property(x => x.Hash).IsRequired().HasColumnType("varbinary(256)").HasMaxLength(256);
                    });
                    break;

                default:
                    throw new InvalidOperationException("Unexpected \"Database\" parameter in appsettings.json. Try \"SqlServer\", \"MySql\" or \"SQLite\".");
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