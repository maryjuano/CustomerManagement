using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolDriving.Data;
using SchoolDriving.Models;

namespace SchoolDriving.Pages.Orders
{
    public class CreateModel : PageModel
    {
        private readonly SchoolDriving.Data.ApplicationDbContext _context;

        public CreateModel(SchoolDriving.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["Students"] = new SelectList(_context.Students.Select(s => new { Id = s.Id, Text = $"{s.FirstName} {s.LastName}" }).AsEnumerable(), "Id", "Text");
            return Page();
        }

        [BindProperty]
        public Order Order { get; set; } 


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            ViewData["Students"] = new SelectList(_context.Students.Select(s => new { Id = s.Id, Text = $"{s.FirstName} {s.LastName}" }).AsEnumerable(), "Id", "Text");
            Order.OrderReference = Guid.NewGuid().ToString().Replace("-", "");
            Order.DateCreated = DateTime.Now;
            Order.LastModified = DateTime.Now;
            ModelState.ClearValidationState(nameof(Models.Order));
            if (!TryValidateModel(Order, nameof(Models.Order)))
            {
               return Page();
            }            

            _context.Orders.Add(Order);
            await _context.SaveChangesAsync();

            return Redirect("/Orders/Edit?id=" + Order.Id);
        }
    }
}
