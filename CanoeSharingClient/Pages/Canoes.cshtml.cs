using Microsoft.AspNetCore.Mvc.RazorPages;
using CanoeSharingClient.Models;
using System.Net.Http.Json;

namespace CanoeSharingClient.Pages
{
    public class CanoesModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;

        public CanoesModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public List<ListingDto> Listings { get; set; } = new();

        public async Task OnGetAsync()
        {
            var client = _clientFactory.CreateClient("ApiClient");
            var response = await client.GetFromJsonAsync<List<ListingDto>>("listings");

            if (response != null)
                Listings = response;
            else
                Listings = new List<ListingDto>();
        }
    }
}