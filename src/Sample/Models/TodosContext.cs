using System.Data.Entity;

namespace Sample.Models
{
    public class ToDosContext : DbContext 
    {
        // DEVELOPMENT ONLY: initialize the database
        static ToDosContext()
        {
            Database.SetInitializer(new ToDoDatabaseInitializer());
        }
        public DbSet<TodoItem> Todos { get; set; }
    }
}