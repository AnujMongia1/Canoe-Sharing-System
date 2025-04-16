namespace CanoeSharingSystemWebAPI.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public int? Rating  { get; set; }
        public string? Description { get; set; }
        public DateTime? ReviewDate { get; set; }
    }
}
