using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SchoolDriving.Data;
using SchoolDriving.Models;

namespace SchoolDriving.Pages.Invoices
{
    public class IndexModel : PageModel
    {
        private readonly SchoolDriving.Data.ApplicationDbContext _context;

        public IndexModel(SchoolDriving.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Invoice> Invoice { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Invoices != null)
            {
                Invoice = await _context.Invoices
                .Include(i => i.Student).ToListAsync();
            }
        }
    }
}
