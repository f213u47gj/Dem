using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TaskRequestDem.Pages
{
    /// <summary>
    /// Класс PageModel для главной страницы (Index).
    /// Обеспечивает обработку GET-запросов и инициализацию логирования.
    /// </summary>
    public class IndexModel : PageModel
    {
        /// <summary>
        /// Логгер для записи информации о работе страницы.
        /// </summary>
        private readonly ILogger<IndexModel> _logger;

        /// <summary>
        /// Конструктор класса <see cref="IndexModel"/>.
        /// Инициализирует логгер для работы со страницей.
        /// </summary>
        /// <param name="logger">Интерфейс логгера для записи сообщений.</param>
        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Обрабатывает GET-запрос для страницы Index.
        /// Используется для отображения основной страницы приложения.
        /// </summary>
        public void OnGet()
        {
            // Метод OnGet вызывается при загрузке страницы.
            // Здесь можно добавить логику, необходимую для отображения данных на главной странице.
        }
    }
}
