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
    /// Модель страницы Razor для отображения подробной информации о заявке.
    /// Загружает данные о заявке из базы данных и отображает их на странице.
    /// </summary>
    public class DetailsModel : PageModel
    {
        /// <summary>
        /// Контекст базы данных для взаимодействия с данными приложения.
        /// </summary>
        private readonly TaskRequestDem.Data.AppDbContext _context;

        /// <summary>
        /// Конструктор класса <see cref="DetailsModel"/>.
        /// Инициализирует контекст базы данных.
        /// </summary>
        /// <param name="context">Контекст базы данных приложения.</param>
        public DetailsModel(TaskRequestDem.Data.AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Свойство для хранения данных о заявке.
        /// Используется для отображения информации на странице.
        /// </summary>
        public Request Request { get; set; } = default!;

        /// <summary>
        /// Обрабатывает GET-запрос для получения и отображения данных о заявке.
        /// Загружает заявку из базы данных по её идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор заявки.</param>
        /// <returns>
        /// Возвращает страницу с подробной информацией о заявке или ошибку,
        /// если заявка не найдена.
        /// </returns>
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            // Проверка на отсутствие идентификатора заявки.
            if (id == null)
            {
                return NotFound();
            }

            // Поиск заявки в базе данных по идентификатору.
            var request = await _context.Requests.FirstOrDefaultAsync(m => m.RequestId == id);

            // Проверка на существование заявки.
            if (request == null)
            {
                return NotFound();
            }
            else
            {
                // Присваивание найденной заявки свойству.
                Request = request;
            }

            // Возвращает страницу с отображением данных заявки.
            return Page();
        }
    }
}
