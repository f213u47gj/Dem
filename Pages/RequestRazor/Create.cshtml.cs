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
    public class CreateModel : PageModel
    {
        private readonly TaskRequestDem.Data.AppDbContext _context;

        public CreateModel(TaskRequestDem.Data.AppDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["ClientId"] = new SelectList(_context.Clients, "ClientId", "ClientId");
        ViewData["ExecutorId"] = new SelectList(_context.Executors, "ExecutorId", "ExecutorId");
        ViewData["StatusId"] = new SelectList(_context.Statuses, "StatusId", "StatusId");
            return Page();
        }

        [BindProperty]
        public Request Request { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            _context.Requests.Add(Request);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
