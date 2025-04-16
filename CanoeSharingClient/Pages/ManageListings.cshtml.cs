using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CanoeSharingClient.Models;

public class ManageListingsModel : PageModel
{
    private readonly IHttpClientFactory _clientFactory;

    public List<ListingDto> Listings { get; set; } = new();
    public string StoreName { get; set; } = "";

    public ManageListingsModel(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }

    public async Task OnGetAsync()
    {
        var storeId = HttpContext.Session.GetInt32("StoreID");
        StoreName = HttpContext.Session.GetString("StoreName") ?? "";

        if (storeId == null) return;

        var client = _clientFactory.CreateClient("ApiClient");
        var response = await client.GetFromJsonAsync<List<ListingDto>>($"listings/mystore/{storeId}");

        if (response != null)
            Listings = response;
    }

    public async Task<IActionResult> OnPostAsync(int listingId)
    {
        var client = _clientFactory.CreateClient("ApiClient");
        await client.DeleteAsync($"listings/{listingId}");
        return RedirectToPage();
    }
}