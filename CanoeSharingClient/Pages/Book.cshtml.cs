using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CanoeSharingClient.Models;

namespace CanoeSharingClient.Pages
{
    public class BookModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int ListingID { get; set; }

        public ListingDto SelectedListing { get; set; } = new();

        [BindProperty]
        public int NumberOfCanoes { get; set; }

        [BindProperty]
        public DateTime BookingStartDate { get; set; }

        [BindProperty]
        public DateTime ReturningDate { get; set; }

        [BindProperty]
        public bool IsBookingConfirmed { get; set; }

        [BindProperty]
        public string? BookingMessage { get; set; }

        public void OnGet()
        {
            SelectedListing = GetMockListingById(ListingID);
        }

        public void OnPostConfirmBooking()
        {
            SelectedListing = GetMockListingById(ListingID);

            // Simulated confirmation
            IsBookingConfirmed = true;
            BookingMessage = "Your booking has been confirmed!";
        }

        private ListingDto GetMockListingById(int id)
        {
            var all = new List<ListingDto>
            {
                new ListingDto { ListingID = 1, ModelName = "Explorer 400", Make = "CanoeWorks", Description = "Lightweight solo canoe", Location = "Dock A", AvailabilityStartDate = DateTime.Today, AvailabilityEndDate = DateTime.Today.AddDays(5) },
                new ListingDto { ListingID = 2, ModelName = "RiverRider", Make = "StreamLine", Description = "Good for long trips", Location = "Dock B", AvailabilityStartDate = DateTime.Today.AddDays(2), AvailabilityEndDate = DateTime.Today.AddDays(8) }
            };

            return all.FirstOrDefault(l => l.ListingID == id) ?? new ListingDto();
        }
    }
}
