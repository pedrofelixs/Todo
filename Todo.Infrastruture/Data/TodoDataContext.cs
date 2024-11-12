using Microsoft.EntityFrameworkCore;
using Todo.Core.Models;
using Todo.Api.Data.Mappings;
using Todo.Infrastruture.Data.Mappings;

namespace Todo.Api.Data
{
    public class TodoDataContext : DbContext
    {
        public TodoDataContext(DbContextOptions<TodoDataContext> options) : base(options)
        {
        }

        public const string connString = "Server=localhost,1433;Database=Todo;User ID=sa;Password=1q2w3e4r@#$;Trusted_Connection=False;TrustServerCertificate=True;";

        public DbSet<TodoItem> TodoItems { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<LogEntry> LogEntries { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseSqlServer(connString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TodoItemMap());
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new LogEntryMap());
        }
    }
}
