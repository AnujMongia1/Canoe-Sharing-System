namespace CanoeSharin.API.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CanoeId { get; set; }
        public DateTime BookingTime { get; set; }

        public User? User { get; set; }
        public Canoe? Canoe { get; set; }
    }

}
