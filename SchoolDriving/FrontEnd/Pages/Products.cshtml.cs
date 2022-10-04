using FrontEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FrontEnd.Pages
{
    public class ProductsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ProductsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Course> Course { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Courses != null)
            {
                Course = await _context.Courses.ToListAsync();
            }
        }
    }
}
