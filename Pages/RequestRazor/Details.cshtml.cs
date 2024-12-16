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
        private readonly TaskRequestDem.Data.AppDbContext _context;

        public DetailsModel(TaskRequestDem.Data.AppDbContext context)
        {
            _context = context;
        }

        public Request Request { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var request = await _context.Requests.FirstOrDefaultAsync(m => m.RequestId == id);
            if (request == null)
            {
                return NotFound();
            }
            else
            {
                Request = request;
            }
            return Page();
        }
    }
}
