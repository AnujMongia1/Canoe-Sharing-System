using CanoeSharin.API.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Listing
{
    [Key]
    public int ListingID { get; set; }

    [ForeignKey("RentalStore")]
    public int? StoreID { get; set; }

    [ForeignKey("User")]
    public int? UserID { get; set; }

    [Required]
    public string Description { get; set; } = "";

    [Required]
    public string ModelName { get; set; } = "";

    [Required]
    public string Make { get; set; } = "";

    [Required]
    public string Location { get; set; } = "";

    [Required]
    public DateTime AvailabilityStartDate { get; set; }

    [Required]
    public DateTime AvailabilityEndDate { get; set; }

    //  Add these navigation properties
    public virtual User? User { get; set; }
    public virtual RentalStore? RentalStore { get; set; }
}

