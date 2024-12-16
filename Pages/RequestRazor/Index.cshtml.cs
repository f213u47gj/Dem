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
    public class IndexModel : PageModel
    {
        private readonly TaskRequestDem.Data.AppDbContext _context;

        public IndexModel(TaskRequestDem.Data.AppDbContext context)
        {
            _context = context;
        }

        public IList<Request> Request { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Request = await _context.Requests
                .Include(r => r.client)
                .Include(r => r.executor)
                .Include(r => r.status).ToListAsync();
        }
    }
}
