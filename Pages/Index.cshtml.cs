using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TaskRequestDem.Pages
{
    /// <summary>
    /// ����� PageModel ��� ������� �������� (Index).
    /// ������������ ��������� GET-�������� � ������������� �����������.
    /// </summary>
    public class IndexModel : PageModel
    {
        /// <summary>
        /// ������ ��� ������ ���������� � ������ ��������.
        /// </summary>
        private readonly ILogger<IndexModel> _logger;

        /// <summary>
        /// ����������� ������ <see cref="IndexModel"/>.
        /// �������������� ������ ��� ������ �� ���������.
        /// </summary>
        /// <param name="logger">��������� ������� ��� ������ ���������.</param>
        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// ������������ GET-������ ��� �������� Index.
        /// ������������ ��� ����������� �������� �������� ����������.
        /// </summary>
        public void OnGet()
        {
            // ����� OnGet ���������� ��� �������� ��������.
            // ����� ����� �������� ������, ����������� ��� ����������� ������ �� ������� ��������.
        }
    }
}
