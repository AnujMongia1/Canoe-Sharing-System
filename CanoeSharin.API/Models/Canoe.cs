namespace CanoeSharin.API.Models
{
    public class Canoe
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Location { get; set; } = "";
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; } = true;
    }


}
