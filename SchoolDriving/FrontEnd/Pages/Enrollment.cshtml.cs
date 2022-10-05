using FrontEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FrontEnd.Pages
{
    public class EnrollmentModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EnrollmentModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Guid CourseId { get; set; } = default!;
        public string CourseName { get; set; }
        public decimal Price { get; set; }
        public double Hours { get; set; }

        [BindProperty]
        public Enrollment Enrollment { get; set; } = default!;

        public async Task<IActionResult> OnGet(Guid courseId)
        {
            if (courseId == null || _context.Courses == null || courseId == Guid.Empty)
            {
                return NotFound();
            }

            var course = await _context.Courses.FindAsync(courseId);

            if (course == null)
            {
                return NotFound();
            }
            CourseName = course.Name;
            Price = course.Price;
            Hours = course.Hours;
            CourseId = course.Id;         
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Guid courseId = CourseId;

            var course = await _context.Courses.FindAsync(courseId);

            if (course == null)
            {
                return NotFound();
            }

            CourseName = course.Name;
            Price = course.Price;
            Hours = course.Hours;
            CourseId = course.Id;

            Enrollment.CreatedOn = DateTime.Now;
            Enrollment.CourseId = CourseId;
            Enrollment.Course = course;
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
        
            return RedirectToPage("Index");
        }
    }
}
