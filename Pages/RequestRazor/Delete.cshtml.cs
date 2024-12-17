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
    /// Модель страницы Razor для удаления заявки.
    /// Загружает заявку для подтверждения удаления и выполняет удаление из базы данных.
    /// </summary>
    public class DeleteModel : PageModel
    {
        /// <summary>
        /// Контекст базы данных для взаимодействия с заявками.
        /// </summary>
        private readonly TaskRequestDem.Data.AppDbContext _context;

        /// <summary>
        /// Конструктор класса <see cref="DeleteModel"/>.
        /// Инициализирует контекст базы данных.
        /// </summary>
        /// <param name="context">Контекст базы данных приложения.</param>
        public DeleteModel(TaskRequestDem.Data.AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Свойство для хранения удаляемой заявки.
        /// Привязывается к данным формы.
        /// </summary>
        [BindProperty]
        public Request Request { get; set; } = default!;

        /// <summary>
        /// Обрабатывает GET-запрос для загрузки данных заявки, которую нужно удалить.
        /// </summary>
        /// <param name="id">Идентификатор заявки.</param>
        /// <returns>
        /// Возвращает страницу для подтверждения удаления или ошибку,
        /// если заявка с указанным идентификатором не найдена.
        /// </returns>
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            // Проверка на отсутствие идентификатора.
            if (id == null)
            {
                return NotFound();
            }

            // Поиск заявки по идентификатору.
            var request = await _context.Requests.FirstOrDefaultAsync(m => m.RequestId == id);

            // Проверка на существование заявки.
            if (request == null)
            {
                return NotFound();
            }
            else
            {
                // Присваивание найденной заявки свойству Request.
                Request = request;
            }

            // Возвращает страницу подтверждения удаления.
            return Page();
        }

        /// <summary>
        /// Обрабатывает POST-запрос для удаления заявки из базы данных.
        /// </summary>
        /// <param name="id">Идентификатор заявки.</param>
        /// <returns>
        /// Перенаправляет на страницу Index после успешного удаления
        /// или возвращает ошибку, если заявка не найдена.
        /// </returns>
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            // Проверка на отсутствие идентификатора.
            if (id == null)
            {
                return NotFound();
            }

            // Поиск заявки в базе данных.
            var request = await _context.Requests.FindAsync(id);

            // Если заявка найдена, удаляем её.
            if (request != null)
            {
                Request = request;
                _context.Requests.Remove(Request);
                await _context.SaveChangesAsync();
            }

            // Перенаправляет на страницу Index после удаления.
            return RedirectToPage("./Index");
        }
    }
}
