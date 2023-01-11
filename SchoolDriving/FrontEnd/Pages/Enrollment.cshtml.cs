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
        public List<IFormFile> FileRequirements { get; set; }

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
            List<Requirements> requirements = new List<Requirements>();

            foreach (var formFile in FileRequirements)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await formFile.CopyToAsync(memoryStream);

                    // Upload the file if less than 2 MB
                    if (memoryStream.Length < 2097152)
                    {
                        requirements.Add(new Requirements()
                        {
                            FileBytes = memoryStream.ToArray(),
                            FileType = formFile.ContentType,
                            FileName = formFile.FileName
                        });
                    }
                    else
                    {
                        ModelState.AddModelError("File", "The file is too large. Max upload is 2 MB");
                    }
                }
            }

            if(requirements.Count == 0)
            {
                ModelState.AddModelError("File", "Please Upload your requirements.");
            }
            

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


            foreach (var requirement in requirements)
            {
                requirement.EnrollmentId = Enrollment.Id;
            }

            _context.Requirements.AddRange(requirements);

            await _context.SaveChangesAsync();

            return Redirect($"/schedule?enrollmentId={Enrollment.Id}#services");
        }

        private async Task ProcessFiles()
        {

        }
    }
}
