using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using SchoolDriving.Data;
using SchoolDriving.Models;

namespace SchoolDriving.Pages.Orders
{
    public class EditModel : PageModel
    {
        private readonly SchoolDriving.Data.ApplicationDbContext _context;

        public EditModel(SchoolDriving.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Order Order { get; set; } = default!;

        [BindProperty]
        public OrderItem OrderItem { get; set; } = default!;


        public async Task<IActionResult> OnGetAsync(Guid? id)
        {

            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.Include(o => o.OrderItems).FirstOrDefaultAsync(m => m.Id == id);

            if (order == null)
            {
                return NotFound();
            }
            Order = order;
            ViewData["Students"] = new SelectList(_context.Students.Select(s => new { Id = s.Id, Text = $"{s.FirstName} {s.LastName}" }).AsEnumerable(), "Id", "Text");
            ViewData["Courses"] = new SelectList(_context.Courses.AsEnumerable(), "Id", "Name");

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAdd(Guid orderId)
        {
            var courses = _context.Courses.AsEnumerable();
            var currentCourse = courses.SingleOrDefault(c => c.Id == OrderItem.CourseId);
            var order = _context.Orders.Include(o => o.OrderItems).Single(o => o.Id == orderId);


            OrderItem.OrderId = orderId;
            OrderItem.ProductPrice = currentCourse?.Price ?? 0M;
            OrderItem.CourseName = currentCourse?.Name ?? string.Empty;

            var existingItem = order.OrderItems.SingleOrDefault(o => o.CourseId == OrderItem.CourseId && o.OrderId == orderId);


            if(existingItem != null)
            {
                existingItem.Quantity = existingItem.Quantity + OrderItem.Quantity;
                _context.OrderItems.Attach(existingItem).State = EntityState.Modified;
            }

            decimal total = 0;

            //recompute
            foreach (var item in order.OrderItems)
            {
                total += item.Quantity * item.ProductPrice;
            }

            order.TotalPrice = total + (OrderItem.Quantity * OrderItem.ProductPrice);

            _context.Orders.Attach(order).State = EntityState.Modified;

            if(existingItem == null)
            {
                _context.OrderItems.Add(OrderItem);
            }

            await _context.SaveChangesAsync();



            ViewData["Students"] = new SelectList(_context.Students.Select(s => new { Id = s.Id, Text = $"{s.FirstName} {s.LastName}" }).AsEnumerable(), "Id", "Text");
            ViewData["Courses"] = new SelectList(courses, "Id", "Name");

            return await OnGetAsync(orderId);
        }

        public async Task<IActionResult> OnPostDelete(Guid orderItemId)
        {
            var orderItem = await _context.OrderItems.FindAsync(orderItemId);

            if (orderItem == null)
            {
                return NotFound();
            }

            _context.OrderItems.Remove(orderItem);
            await _context.SaveChangesAsync();

            var order = _context.Orders.Include(o => o.OrderItems).Single(o => o.Id == orderItem.OrderId);

            decimal total = 0;

            //recompute
            foreach (var item in order.OrderItems)
            {
                total += item.Quantity * item.ProductPrice;
            }

            order.TotalPrice = total;

            _context.Orders.Attach(order).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return await OnGetAsync(orderItem.OrderId);
        }

        private bool OrderExists(Guid id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }


    }
}
