using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TaskRequestDem.Data;
using TaskRequestDem.Model;

namespace TaskRequestDem.Pages.RequestRazor
{
    /// <summary>
    /// Модель страницы Razor для отображения списка заявок.
    /// Загружает данные заявок из базы данных, включая связанные сущности клиента, исполнителя и статуса.
    /// </summary>
    public class IndexModel : PageModel
    {
        private readonly TaskRequestDem.Data.AppDbContext _context;

        /// <summary>
        /// Конструктор модели страницы. Принимает контекст базы данных через внедрение зависимостей.
        /// </summary>
        /// <param name="context">Контекст базы данных приложения.</param>
        public IndexModel(TaskRequestDem.Data.AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Список заявок, загружаемых из базы данных.
        /// Используется для передачи данных на страницу Razor.
        /// </summary>
        public IList<Request> Request { get;set; } = default!;

        /// <summary>
        /// Асинхронный метод, вызываемый при GET-запросе к странице.
        /// Загружает данные заявок из базы данных, включая связанные сущности (клиент, исполнитель, статус).
        /// </summary>
        /// <returns>Задача (Task), представляющая процесс выполнения метода.</returns>
        public async Task OnGetAsync()
        {
            Request = await _context.Requests
                .Include(r => r.client)
                .Include(r => r.executor)
                .Include(r => r.status).ToListAsync();
        }
    }
}
