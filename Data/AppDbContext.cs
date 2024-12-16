using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TaskRequestDem.Model;

namespace TaskRequestDem.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Status> Statuses { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Executor> Executors { get; set; }

    }
}
