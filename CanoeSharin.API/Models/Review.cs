namespace CanoeSharin.API.Models
{
    public class Review
    {
        public int ReviewId { get; set; }
        public int BookingId { get; set; }
        public int UserId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; } = "";
        public DateTime ReviewDate { get; set; }

        public Booking? Booking { get; set; }
        public User? User { get; set; }
    }
}