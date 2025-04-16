using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CanoeSharingClient.Models;

public class EditListingModel : PageModel
{
    private readonly IHttpClientFactory _clientFactory;

    [BindProperty]
    public ListingDto Listing { get; set; } = new();

    public EditListingModel(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var client = _clientFactory.CreateClient("ApiClient");
        Listing = await client.GetFromJsonAsync<ListingDto>($"listings/{id}");

        if (Listing == null) return RedirectToPage("/ManageListings");
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();

        var client = _clientFactory.CreateClient("ApiClient");
        var response = await client.PutAsJsonAsync($"listings/{Listing.ListingID}", Listing);

        return RedirectToPage("/ManageListings");
    }
}