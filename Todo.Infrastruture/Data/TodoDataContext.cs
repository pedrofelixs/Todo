using Microsoft.EntityFrameworkCore;
using Todo.Core.Models;
using Microsoft.Extensions.Options;


namespace Todo.Api.Data
{
    public class TodoDataContext : DbContext
    {

        public TodoDataContext(DbContextOptions<TodoDataContext> options): base(options)
        {

        }
        private const string connString = "Server = localhost,1433; Database=ItaliaMi;User ID = sa; Password=1q2w3e4r@#$;Trusted_Connection=False; TrustServerCertificate=True;";

        public DbSet<TodoItem> TodoItems { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(connString);
        }
    }
}
