namespace CanoeSharingClient.Models
{
    public class ListingDto
    {
        public int ListingID { get; set; }
        public string ModelName { get; set; } = "";
        public string Make { get; set; } = "";
        public string Description { get; set; } = "";
        public string Location { get; set; } = "";
        public DateTime AvailabilityStartDate { get; set; }
        public DateTime AvailabilityEndDate { get; set; }
        public string StoreName { get; set; } = "";
    }
}