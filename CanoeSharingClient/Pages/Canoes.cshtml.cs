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
            //  Uncomment this block when API is working
            /*
            var client = _clientFactory.CreateClient("ApiClient");
            var response = await client.GetFromJsonAsync<List<ListingDto>>("listings");

            if (response != null)
                Listings = response;
            */

            //  Mock listings for UI development (no API required)
            Listings = new List<ListingDto>
            {
                new ListingDto
                {
                    ListingID = 1,
                    ModelName = "Explorer Mayank",
                    Make = "CanoeWorks",
                    Description = "Lightweight solo canoe, ideal for lakes.",
                    Location = "Blue Lake Dock",
                    AvailabilityStartDate = DateTime.Today,
                    AvailabilityEndDate = DateTime.Today.AddDays(5)
                },
                new ListingDto
                {
                    ListingID = 2,
                    ModelName = "Ocean Voyager",
                    Make = "RiverRider",
                    Description = "Built for longer journeys with extra storage.",
                    Location = "Harbour Bay",
                    AvailabilityStartDate = DateTime.Today.AddDays(2),
                    AvailabilityEndDate = DateTime.Today.AddDays(8)
                }
            };
        }
    }
}
