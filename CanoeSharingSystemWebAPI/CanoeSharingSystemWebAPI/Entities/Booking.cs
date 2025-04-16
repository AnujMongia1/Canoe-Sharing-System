namespace CanoeSharingSystemWebAPI.Entities
{
    public class Booking
    {
        public int Id { get; set; }
        public DateTime? BookingStartDate { get; set; }
        public DateTime? BookingEndDate { get; set; }
    }
}
