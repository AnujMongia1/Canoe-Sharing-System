using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CanoeSharingClient.Pages
{
    public class CanoesModel : PageModel
    {
        [TempData]
        public string? Username { get; set; }

        public void OnGet()
        {
            // Optional: add logic to fetch user-specific data here
        }
    }

}
