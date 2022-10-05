using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolDriving.Data;
using SchoolDriving.Models;

namespace SchoolDriving.Pages.Enrollments
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
            ViewData["Courses"] = new SelectList(_context.Courses.AsEnumerable(), "Id", "Name");
           
            return Page();
        }

        [BindProperty]
        public Enrollment Enrollment { get; set; }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            ViewData["Courses"] = new SelectList(_context.Courses.AsEnumerable(), "Id", "Name");

            Enrollment.CreatedOn = DateTime.Now;
            Enrollment.Reference = Guid.NewGuid().ToString().Replace("-", "");

            ModelState.ClearValidationState(nameof(Models.Enrollment));
            if (!TryValidateModel(Enrollment, nameof(Models.Enrollment)))
            {
                return Page();
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Enrollment.Add(Enrollment);
            await _context.SaveChangesAsync();

            return Redirect("/Enrollments/Edit?id=" + Enrollment.Id);
        }
    }
}
