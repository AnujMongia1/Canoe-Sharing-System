namespace CanoeSharingSystemWebAPI.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Password { get; set; }
        public ICollection<Booking>? Bookings { get; set; }
        public ICollection<Listing>? Listings { get; set; }
        public ICollection<Review>? Reviews { get; set; }
    }
}
