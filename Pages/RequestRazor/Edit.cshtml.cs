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
    public class EditModel : PageModel
    {
        private readonly TaskRequestDem.Data.AppDbContext _context;

        public EditModel(TaskRequestDem.Data.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Request Request { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var request =  await _context.Requests.FirstOrDefaultAsync(m => m.RequestId == id);
            if (request == null)
            {
                return NotFound();
            }
            Request = request;
           ViewData["ClientId"] = new SelectList(_context.Clients, "ClientId", "ClientId");
           ViewData["ExecutorId"] = new SelectList(_context.Executors, "ExecutorId", "ExecutorId");
           ViewData["StatusId"] = new SelectList(_context.Statuses, "StatusId", "StatusId");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            _context.Attach(Request).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestExists(Request.RequestId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool RequestExists(int id)
        {
            return _context.Requests.Any(e => e.RequestId == id);
        }
    }
}
