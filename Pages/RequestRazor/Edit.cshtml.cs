using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TaskRequestDem.Data;
using TaskRequestDem.Model;

namespace TaskRequestDem.Pages.RequestRazor
{
    /// <summary>
    /// Модель страницы Razor для редактирования существующей заявки.
    /// Позволяет загружать данные заявки, редактировать их и сохранять изменения в базе данных.
    /// </summary>
    public class EditModel : PageModel
    {
        /// <summary>
        /// Контекст базы данных для взаимодействия с данными приложения.
        /// </summary>
        private readonly TaskRequestDem.Data.AppDbContext _context;

        /// <summary>
        /// Конструктор класса <see cref="EditModel"/>.
        /// Инициализирует контекст базы данных.
        /// </summary>
        /// <param name="context">Контекст базы данных приложения.</param>
        public EditModel(TaskRequestDem.Data.AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Свойство для хранения редактируемой заявки.
        /// Привязывается к данным формы.
        /// </summary>
        [BindProperty]
        public Request Request { get; set; } = default!;

        /// <summary>
        /// Обрабатывает GET-запрос для загрузки данных заявки по её идентификатору.
        /// Загружает данные заявки и подготавливает выпадающие списки для выбора значений.
        /// </summary>
        /// <param name="id">Идентификатор заявки.</param>
        /// <returns>Результат выполнения страницы или ошибка, если заявка не найдена.</returns>
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            // Проверка на отсутствие идентификатора.
            if (id == null)
            {
                return NotFound();
            }

            // Поиск заявки в базе данных по её идентификатору.
            var request = await _context.Requests.FirstOrDefaultAsync(m => m.RequestId == id);
            if (request == null)
            {
                return NotFound();
            }

            // Присваивание данных заявки.
            Request = request;

            // Заполнение выпадающих списков для клиентов, исполнителей и статусов.
            ViewData["ClientId"] = new SelectList(_context.Clients, "ClientId", "ClientId");
            ViewData["ExecutorId"] = new SelectList(_context.Executors, "ExecutorId", "ExecutorId");
            ViewData["StatusId"] = new SelectList(_context.Statuses, "StatusId", "StatusId");

            return Page();
        }

        /// <summary>
        /// Обрабатывает POST-запрос для сохранения изменений в редактируемой заявке.
        /// Обновляет данные заявки в базе данных.
        /// </summary>
        /// <returns>Перенаправление на страницу Index после успешного сохранения изменений.</returns>
        public async Task<IActionResult> OnPostAsync()
        {
            // Устанавливает состояние заявки как "изменённое".
            _context.Attach(Request).State = EntityState.Modified;

            try
            {
                // Сохраняет изменения в базе данных.
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Проверка на существование заявки при конфликте параллелизма.
                if (!RequestExists(Request.RequestId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            // Перенаправление на страницу Index после успешного редактирования.
            return RedirectToPage("./Index");
        }

        /// <summary>
        /// Проверяет существование заявки в базе данных.
        /// </summary>
        /// <param name="id">Идентификатор заявки.</param>
        /// <returns>True, если заявка существует; иначе False.</returns>
        private bool RequestExists(int id)
        {
            return _context.Requests.Any(e => e.RequestId == id);
        }
    }
}
