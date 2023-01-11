using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolDriving.Models;
using System.Runtime.Intrinsics.Arm;

namespace SchoolDriving.Pages.Invoices
{
    public class EditModel : PageModel
    {
        private readonly SchoolDriving.Data.ApplicationDbContext _context;

        public EditModel(SchoolDriving.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Invoice Invoice { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.Invoices == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoices.Include(i => i.OrderItems).Include(p => p.Payment).FirstOrDefaultAsync(m => m.Id == id);
            if (invoice == null)
            {
                return NotFound();
            }
            Invoice = invoice;

            ViewData["Students"] = new SelectList(_context.Students.Select(s => new { Id = s.Id, Text = $"{s.FirstName} {s.LastName}" }).AsEnumerable(), "Id", "Text");

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {

            if (Invoice.PaymentId == Guid.Empty || Invoice.PaymentId == null)
            {
                ModelState.AddModelError("PaymentReference", "Payment Reference cannot be blank");
            }

            if (Invoice.PaymentDate == DateTime.MinValue || Invoice.PaymentDate == null)
            {
                ModelState.AddModelError("PaymentDate", "Invalid Payment Date.");
            }

            if (!ModelState.IsValid)
            {
                Invoice = await _context.Invoices.Include(o => o.OrderItems).FirstOrDefaultAsync(m => m.Id == Invoice.Id);
                return Page();
            }

            Invoice.LastModified = DateTime.Now;
            Invoice.IsPaid = true;
            _context.Attach(Invoice).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvoiceExists(Invoice.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnGetGeneratePdf(Guid? id)
        {
            var Renderer = new IronPdf.ChromePdfRenderer();

            if (id == null || id == Guid.Empty)
            {
                return NotFound();
            }

            var invoice = await _context.Invoices.Include(s => s.Student).FirstOrDefaultAsync(i => i.Id == id);

            if (invoice == null)
            {
                return NotFound();
            }          

            // Create a PDF from a URL or local file path
            using var pdf = Renderer.RenderUrlAsPdf($"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}/Invoices/DownloadPdf?id={id.ToString()}");

            if (pdf == null)
            {
                return NotFound();
            }

            ////Send the File to Download.
            return File(pdf.BinaryData, "application/pdf", $"{invoice.Student.LastName}-{invoice.Student.FirstName}-{invoice.DateCreated.ToString("MM-dd-yyyy")}.pdf");
        }

        private bool InvoiceExists(Guid id)
        {
            return _context.Invoices.Any(e => e.Id == id);
        }
    }
}
