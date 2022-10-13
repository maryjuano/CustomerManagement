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

namespace SchoolDriving.Pages.Invoices
{
    public class CreateModel : PageModel
    {
        private readonly SchoolDriving.Data.ApplicationDbContext _context;

        public CreateModel(SchoolDriving.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGet(Guid orderId)
        {
            var order = await _context.Orders.Include(o => o.OrderItems).SingleOrDefaultAsync(o => o.Id == orderId);
            
            if (order == null)
            {
                return NotFound();
            }

            if (order.OrderItems.Count <= 0)
            {
                return BadRequest();
            }

            var enrollment = await _context.Enrollment.Include(e => e.Payment).SingleOrDefaultAsync(e => e.Id == order.EnrollmentId);

            var newInvoice = new Invoice()
            {
                OrderId = orderId,
                IsPaid = false,
                TotalPrice = order.TotalPrice,
                StudentId = order.StudentId,
                DateCreated = DateTime.Now,
                LastModified = DateTime.Now,
                OrderItems = order.OrderItems,
                PaymentId = Guid.Parse(enrollment.PaymentId.ToString()),
                PaymentDate = enrollment.Payment?.CreatedOn
            };

            _context.Invoices.Add(newInvoice);
            await _context.SaveChangesAsync();
            order.InvoiceId = newInvoice.Id;
            _context.Orders.Attach(order).State = EntityState.Modified;
            await _context.SaveChangesAsync();


            return Redirect("/Invoices/Edit?id=" + newInvoice.Id);
        }

    }
}
