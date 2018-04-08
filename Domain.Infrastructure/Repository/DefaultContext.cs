namespace Domain.Infrastructure.Repository
{
    using Entities;
    using Microsoft.EntityFrameworkCore;



    public sealed class DefaultContext : DbContext
    {
        public DefaultContext()
        {
            Database.EnsureCreated();
        }



        public DbSet<Employee> Employees { get; set; }

        public DbSet<EmployeeRegistration> EmployeeRegistrations { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("Server=localhost;Database=EmployeeRegistrator;User=root;Password=12345678;SslMode=none");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}