using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TaskRequestDem.Data;
using TaskRequestDem.Model;

namespace TaskRequestDem.Pages.RequestRazor
{
    /// <summary>
    /// Класс Razor PageModel для создания новой заявки.
    /// Обеспечивает функциональность для загрузки формы создания заявки и сохранения её в базу данных.
    /// </summary>
    public class CreateModel : PageModel
    {
        /// <summary>
        /// Контекст базы данных для взаимодействия с данными приложения.
        /// </summary>
        private readonly TaskRequestDem.Data.AppDbContext _context;

        /// <summary>
        /// Конструктор класса <see cref="CreateModel"/>.
        /// Инициализирует контекст базы данных.
        /// </summary>
        /// <param name="context">Контекст базы данных приложения.</param>
        public CreateModel(TaskRequestDem.Data.AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Обрабатывает GET-запрос для загрузки формы создания новой заявки.
        /// Заполняет выпадающие списки для выбора клиентов, исполнителей и статусов.
        /// </summary>
        /// <returns>Результат Razor Page.</returns>
        public IActionResult OnGet()
        {
            ViewData["ClientId"] = new SelectList(_context.Clients, "ClientId", "ClientId");
            ViewData["ExecutorId"] = new SelectList(_context.Executors, "ExecutorId", "ExecutorId");
            ViewData["StatusId"] = new SelectList(_context.Statuses, "StatusId", "StatusId");
            return Page();
        }

        /// <summary>
        /// Модель заявки, привязанная к данным формы.
        /// Используется для создания новой сущности заявки.
        /// </summary>
        [BindProperty]
        public Request Request { get; set; } = default!;

        /// <summary>
        /// Обрабатывает POST-запрос для сохранения новой заявки в базе данных.
        /// Добавляет новую заявку в контекст базы данных и сохраняет изменения асинхронно.
        /// </summary>
        /// <returns>
        /// Перенаправление на страницу Index после успешного сохранения новой заявки.
        /// </returns>
        public async Task<IActionResult> OnPostAsync()
        {
            // Добавляет новую заявку в таблицу Requests контекста базы данных.
            _context.Requests.Add(Request);

            // Асинхронно сохраняет изменения в базе данных.
            await _context.SaveChangesAsync();

            // Перенаправляет на страницу Index после сохранения заявки.
            return RedirectToPage("./Index");
        }
    }
}
