namespace Domain.Infrastructure.Repository
{
    using Entities.Building;
    using System;
    using Entities.Employee;
    using Entities.Entrance;
    using Entities.Registration;
    using Entities.User;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;



    public sealed class DefaultContext : DbContext
    {
        private readonly EntityFrameworkSettings _settings;
        private readonly ILoggerFactory _loggerFactory;

        
        
        public DefaultContext(
            ILoggerFactory loggerFactory,
            EntityFrameworkSettings settings)
        {
            _loggerFactory = loggerFactory;
            _settings = settings;
            
            Database.EnsureCreated();
        }



        public DbSet<Employee> Employees { get; set; }

        public DbSet<Registration> Registrations { get; set; }

        public DbSet<User> Users { get; set; }
        
        public DbSet<Building> Buildings { get; set; }
        
        public DbSet<Entrance> Entrances { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            switch (_settings.Database)
            {
                case Repository.Database.SqlServer:
                    optionsBuilder.UseSqlServer(_settings.ConnectionStrings.SqlServer);
                    break;

                case Repository.Database.MySql:
                    optionsBuilder.UseMySQL(_settings.ConnectionStrings.MySql);
                    break;

                case Repository.Database.SqLite:
                    optionsBuilder.UseSqlite(_settings.ConnectionStrings.SqLite);
                    break;

                case Repository.Database.InMemory:
                    optionsBuilder.UseInMemoryDatabase(_settings.ConnectionStrings.InMemory);
                    break;

                default:
                    throw new InvalidOperationException("Unexpected \"Database\" parameter in appsettings.json.");
            }

            optionsBuilder.UseLoggerFactory(_loggerFactory);

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().Property(x => x.NeedNotify).HasConversion<int>();
            modelBuilder.Entity<Employee>().Property(x => x.WorkplacePresenceRequired).HasConversion<int>();
            
            switch (_settings.Database)
            {
                case Repository.Database.SqlServer:
                    modelBuilder.Entity<User>().OwnsOne(x => x.Password, pw =>
                    {
                        pw.Property(x => x.Salt).IsRequired().HasColumnType("varbinary(40)").HasMaxLength(40);
                        pw.Property(x => x.Hash).IsRequired().HasColumnType("varbinary(256)").HasMaxLength(256);
                    });
                    break;

                case Repository.Database.MySql:
                    modelBuilder.Entity<User>().OwnsOne(x => x.Password, pw =>
                    {
                        pw.Property(x => x.Salt).IsRequired().HasColumnType("MEDIUMBLOB").HasMaxLength(40);
                        pw.Property(x => x.Hash).IsRequired().HasColumnType("MEDIUMBLOB").HasMaxLength(256);
                    });
                    break;

                case Repository.Database.SqLite:
                    modelBuilder.Entity<User>().OwnsOne(x => x.Password, pw =>
                    {
                        pw.Property(x => x.Salt).IsRequired().HasColumnType("BLOB").HasMaxLength(40);
                        pw.Property(x => x.Hash).IsRequired().HasColumnType("BLOB").HasMaxLength(256);
                    });
                    break;

                case Repository.Database.InMemory:
                    modelBuilder.Entity<User>().OwnsOne(x => x.Password, pw =>
                    {
                        pw.Property(x => x.Salt).IsRequired().HasColumnType("varbinary(40)").HasMaxLength(40);
                        pw.Property(x => x.Hash).IsRequired().HasColumnType("varbinary(256)").HasMaxLength(256);
                    });
                    break;

                default:
                    throw new InvalidOperationException("Unexpected \"Database\" parameter in appsettings.json.");
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}