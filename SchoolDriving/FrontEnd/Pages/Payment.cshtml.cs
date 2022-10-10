using Braintree;
using FrontEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FrontEnd.Pages
{
    public class PaymentModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public PaymentModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public string ClientToken { get; set; }

        [BindProperty]
        public string Nonce { get; set; }

        [BindProperty]
        public Guid EnrollmentId { get; set; }

        public Enrollment Enrollment { get; set; } = default!;

        public Schedule Schedule { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid enrollmentId)
        {
            Enrollment = await _context.Enrollment.Include(e => e.Course).SingleOrDefaultAsync(e => e.Id == enrollmentId);

            if (Enrollment == null)
            {
                Redirect("/");
            }

            Schedule = await _context.Schedules.FindAsync(Enrollment.ScheduleId);

            if (Schedule == null)
            {
                Redirect("/");
            }

            var gateway = new BraintreeGateway
            {
                Environment = Braintree.Environment.SANDBOX,
                MerchantId = "2km4wrznxbrk65hr",
                PublicKey = "q9k94m2nsbxswswr",
                PrivateKey = "37b1508892eff6e7267aa7979ed54676"
            };

            ClientToken = await gateway.ClientToken.GenerateAsync();
            //EnrollmentId = enrollmentId;

            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            Enrollment = await _context.Enrollment.Include(e => e.Course).SingleOrDefaultAsync(e => e.Id == EnrollmentId);

            if (Enrollment == null)
            {
                Redirect("/");
            }           

            Schedule = await _context.Schedules.FindAsync(Enrollment.ScheduleId);

            if (Schedule == null)
            {
                Redirect("/");
            }

            var gateway = new BraintreeGateway
            {
                Environment = Braintree.Environment.SANDBOX,
                MerchantId = "2km4wrznxbrk65hr",
                PublicKey = "q9k94m2nsbxswswr",
                PrivateKey = "37b1508892eff6e7267aa7979ed54676"
            };

            var request = new TransactionRequest
            {
                Amount = Enrollment.Course.Price,
                PaymentMethodNonce = Nonce,            
                Options = new TransactionOptionsRequest
                {
                    SubmitForSettlement = true
                }
            };

            Result<Transaction> transactionResult = gateway.Transaction.Sale(request);
            Transaction transaction = gateway.TestTransaction.Settle(transactionResult.Target.Id);

            Enrollment.PaymentReference = transaction.Id;
            _context.Attach(Enrollment).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Redirect("/paymentsuccess?refId=" + transaction.Id);
        }

    }
}
