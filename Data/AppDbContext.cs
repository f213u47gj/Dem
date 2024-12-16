using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TaskRequestDem.Model;

namespace TaskRequestDem.Data
{
    /// <summary>
    /// Контекст базы данных для работы с сущностями системы.
    /// Включает наборы данных для работы с сущностями статусов, заявок, клиентов и исполнителей.
    /// </summary>
    public class AppDbContext : DbContext
    {
        /// <summary>
        /// Инициализирует новый экземпляр контекста базы данных с указанными параметрами.
        /// </summary>
        /// <param name="options">Параметры конфигурации для контекста базы данных.</param>
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// Набор данных для работы с сущностями статусов.
        /// </summary>
        public DbSet<Status> Statuses { get; set; }

        /// <summary>
        /// Набор данных для работы с сущностями заявок.
        /// </summary>
        public DbSet<Request> Requests { get; set; }

        /// <summary>
        /// Набор данных для работы с сущностями клиентов.
        /// </summary>
        public DbSet<Client> Clients { get; set; }

        /// <summary>
        /// Набор данных для работы с сущностями исполнителей.
        /// </summary>
        public DbSet<Executor> Executors { get; set; }
    }
}
