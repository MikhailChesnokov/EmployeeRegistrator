namespace Domain.Infrastructure.Repository
{
    using Components.Password;
    using Entities.Employee;
    using Entities.Registration;
    using Entities.User;
    using Microsoft.EntityFrameworkCore;



    public sealed class DefaultContext : DbContext
    {
        public DefaultContext()
        {
            Database.EnsureCreated();
        }



        public DbSet<Employee> Employees { get; set; }

        public DbSet<Registration> Registrations { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Password> Passwords { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("Server=localhost;Database=EmployeeRegistrator;User=root;Password=12345678;SslMode=none");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>().HasOne(x => x.Password);

            modelBuilder.Entity<Password>().Property(x => x.Salt).IsRequired().HasColumnType("MEDIUMBLOB").HasMaxLength(40);
            modelBuilder.Entity<Password>().Property(x => x.Hash).IsRequired().HasColumnType("MEDIUMBLOB").HasMaxLength(256);

            base.OnModelCreating(modelBuilder);
        }
    }
}