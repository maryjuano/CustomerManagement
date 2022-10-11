using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FrontEnd.Pages
{
    public class PaymentSuccessModel : PageModel
    {
        public string ReferenceId { get; set; }
        public async Task<IActionResult> OnGet(string refId)
        {
            if (string.IsNullOrWhiteSpace(refId))
            {
                return Redirect("/");
            }

            ReferenceId = refId;
            return Page();
        }
    }
}
