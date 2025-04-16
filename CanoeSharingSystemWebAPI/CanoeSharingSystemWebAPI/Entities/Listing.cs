namespace CanoeSharingSystemWebAPI.Entities
{
    public class Listing
    {
        public int Id { get; set; }
        public string? Modelname { get; set; }
        public string? Description { get; set; }
        public string? Make { get; set; }
        public string? Location { get; set; }
        public DateTime? AvailabilityStartDate { get; set; }
        public DateTime? AvailabilityEndDate { get; set; }
        public int? RentalStoreId { get; set; }
        public RentalStore? RentalStore { get; set; }
        public int? UserId { get; set; }
        public User? User { get; set; }
        public ICollection<Booking>? Bookings { get; set; }

    }
}
