using FrontEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FrontEnd.Pages
{
    public class ScheduleModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ScheduleModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Guid EnrollmentId { get; set; }
        [BindProperty]
        public Guid ScheduleId { get; set; }

        public IList<Schedule> Schedules { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid enrollmentId)
        {           
            var enrollment = await _context.Enrollment.FindAsync(enrollmentId);

            if(enrollment == null)
            {
                return Redirect("/");
            }

            EnrollmentId = enrollmentId;

            if (_context.Courses != null)
            {
                Schedules = await _context.Schedules.Where(s => s.CourseId == enrollment.CourseId && s.StudentId == Guid.Empty || s.StudentId == null && (s.StartDate.Date > DateTime.Now.Date) && (s.EndDate.Date > DateTime.Now.Date)).ToListAsync();
            }

            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            var enrollment = await _context.Enrollment.FindAsync(EnrollmentId);



            enrollment.ScheduleId = ScheduleId;
            _context.Attach(enrollment).State = EntityState.Modified;
            await _context.SaveChangesAsync();


            if (_context.Courses != null)
            {
                Schedules = await _context.Schedules.Where(s => s.CourseId == enrollment.CourseId && s.StudentId == Guid.Empty || s.StudentId == null && (s.StartDate.Date > DateTime.Now.Date) && (s.EndDate.Date > DateTime.Now.Date)).ToListAsync();
            }

            return Redirect($"/payment?enrollmentId={enrollment.Id}#services");
        }
    }
}
