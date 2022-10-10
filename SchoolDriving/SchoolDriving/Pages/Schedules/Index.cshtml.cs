using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SchoolDriving.Models;

namespace SchoolDriving.Pages.Schedules
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly SchoolDriving.Data.ApplicationDbContext _context;

        public IndexModel(SchoolDriving.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Schedule> Schedules { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Orders != null)
            {
                Schedules = await _context.Schedules.Include(s => s.Instructor).Include(s => s.Course).ToListAsync();
            }
        }
    }
}
