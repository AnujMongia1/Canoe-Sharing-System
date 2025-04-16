namespace CanoeSharingSystemWebAPI.Entities
{
    public class Booking
    {
        public int Id { get; set; }
        public DateTime? BookingStartDate { get; set; }
        public DateTime? BookingEndDate { get; set; }
        public int? UserId { get; set; }
        public User? User { get; set; }
        public int? ListingId { get; set; }
        public Listing? Listing { get; set; }
        public ICollection<Review>? Reviews { get; set; }
    }
}
