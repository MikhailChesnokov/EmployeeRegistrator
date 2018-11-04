namespace Domain.Infrastructure.Repository
{
    public enum Database
    {
        SqlServer = 1,
        MySql = 2,
        SqLite = 3,
        InMemory = 4
    }
    
    public class EntityFrameworkSettings
    {
        public Database Database { get; set; }

        public ConnectionStrings ConnectionStrings { get; set; }
    }
}