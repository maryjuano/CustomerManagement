using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolDriving.Models;

namespace SchoolDriving.Pages.Schedules
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
            ViewData["Instructors"] = new SelectList(_context.Instructors.Select(s => new { Id = s.Id, Text = $"{s.FirstName} {s.LastName}" }).AsEnumerable(), "Id", "Text");           
            return Page();
        }

        [BindProperty]
        public Schedule Schedule { get; set; }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            ViewData["Courses"] = new SelectList(_context.Courses.AsEnumerable(), "Id", "Name");
            ViewData["Instructors"] = new SelectList(_context.Instructors.Select(s => new { Id = s.Id, Text = $"{s.FirstName} {s.LastName}" }).AsEnumerable(), "Id", "Text");

            if(Schedule.StartDate > Schedule.EndDate)
            {
                ModelState.AddModelError("InvalidSchedule", "Start Date cannot be greater than End Date");
                return Page();
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            // bool overlap = a.start < b.end && b.start < a.end;
            bool overlap = await _context.Schedules.AnyAsync(s => s.StartDate < Schedule.EndDate && Schedule.StartDate < s.EndDate && s.CourseId == Schedule.CourseId && s.InstructorId == Schedule.InstructorId);

            if(overlap)
            {
                ModelState.AddModelError("ScheduleOverlap", "There is a conflict of Schedule for the  selected Instructor and Course");
                return Page();
            }

            _context.Schedules.Add(Schedule);

            await _context.SaveChangesAsync();
           
            return RedirectToPage("./Index");
        }
    }
}
