using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CanoeSharingClient.Models;

public class AddListingModel : PageModel
{
    private readonly IHttpClientFactory _clientFactory;

    [BindProperty]
    public ListingDto NewListing { get; set; } = new();

    public AddListingModel(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var storeId = HttpContext.Session.GetInt32("StoreID");
        if (storeId == null) return RedirectToPage("/StoreLogin");

        var listing = new
        {
            StoreID = storeId,
            ModelName = NewListing.ModelName,
            Make = NewListing.Make,
            Description = NewListing.Description,
            Location = NewListing.Location,
            AvailabilityStartDate = NewListing.AvailabilityStartDate,
            AvailabilityEndDate = NewListing.AvailabilityEndDate
        };

        var client = _clientFactory.CreateClient("ApiClient");
        var response = await client.PostAsJsonAsync("listings", listing);

        return RedirectToPage("/ManageListings");
    }
}