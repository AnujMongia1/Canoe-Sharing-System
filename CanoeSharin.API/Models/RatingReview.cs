namespace CanoeSharin.API.Models
{
    public class RatingReview
    {
        public int ReviewID { get; set; }  // Primary Key
        public int BookingID { get; set; } // Foreign Key
        public int UserID { get; set; }    // Foreign Key
        public int Rating { get; set; }
        public string CommentDescription { get; set; }
        public DateTime ReviewDate { get; set; }
    }
}