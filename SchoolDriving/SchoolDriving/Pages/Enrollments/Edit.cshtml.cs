using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolDriving.Data;
using SchoolDriving.Models;

namespace SchoolDriving.Pages.Enrollments
{
    public class EditModel : PageModel
    {
        private readonly SchoolDriving.Data.ApplicationDbContext _context;

        public EditModel(SchoolDriving.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Enrollment Enrollment { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.Enrollment == null)
            {
                return NotFound();
            }

            var enrollment =  await _context.Enrollment.Include(e => e.Course).FirstOrDefaultAsync(m => m.Id == id);
            if (enrollment == null)
            {
                return NotFound();
            }
            Enrollment = enrollment;
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Id");

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Id");

            Enrollment = await _context.Enrollment.Include(e => e.Course).FirstOrDefaultAsync(m => m.Id == Enrollment.Id);          

            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Enrollment.Approved)
            {
                return Page();
            }

            using var transaction = _context.Database.BeginTransaction();

            Enrollment.Approved = true;

            var student = new Student()
            {
                FirstName = Enrollment.FirstName,
                MiddleName = Enrollment.MiddleName,
                LastName = Enrollment.LastName,
                Email = Enrollment.Email,
                Mobile = Enrollment.Mobile,
                StreetNumber = Enrollment.StreetNumber,
                Barangay = Enrollment.Barangay,
                Municipality = Enrollment.Municipality,
                Province = Enrollment.Province,
                ZipCode = Enrollment.ZipCode,
                BirthDate = Enrollment.BirthDate,
                Age = Enrollment.Age,
                Sex = Enrollment.Sex,
                CivilStatus = Enrollment.CivilStatus,
                LicenseNumber = Enrollment.LicenseNumber,
                EducationalAttainment = Enrollment.EducationalAttainment,
                EmploymentStatus = Enrollment.EmploymentStatus,
                CreatedOn = DateTime.Now
            };

            _context.Students.Add(student);

            _context.Attach(Enrollment).State = EntityState.Modified;

            var order = new Order();

            try
            {
                await _context.SaveChangesAsync();

              
                order.Student = student;
                order.StudentId = student.Id;
                order.OrderReference = Guid.NewGuid().ToString().Replace("-", "");
                order.DateCreated = DateTime.Now;
                order.LastModified = DateTime.Now;
                order.EnrollmentId = Enrollment.Id;
                order.Enrollment = Enrollment;
                order.TotalPrice = Enrollment.Course.Price;

                order.OrderItems.Add(new OrderItem()
                {
                     CourseId = Enrollment.CourseId,
                     CourseName = Enrollment.Course.Name,
                     ProductPrice = Enrollment.Course.Price,
                     Quantity = 1,
                     Professional = false
                });


                _context.Orders.Add(order);
                
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EnrollmentExists(Enrollment.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
                await transaction.RollbackAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
            }

            return Redirect("/Orders/Edit?id=" + order.Id);
        }

        private bool EnrollmentExists(Guid id)
        {
          return _context.Enrollment.Any(e => e.Id == id);
        }
    }
}
