using CanoeSharin.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class ListingsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ListingsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: /api/listings
    [HttpGet]
    public async Task<IActionResult> GetAllListings()
    {
        var listings = await _context.Listings
            .Include(l => l.RentalStore)
            .Select(l => new
            {
                l.ListingID,
                l.ModelName,
                l.Make,
                l.Description,
                l.Location,
                l.AvailabilityStartDate,
                l.AvailabilityEndDate,
                l.StoreID
            })
            .ToListAsync();

        return Ok(listings);
    }

    // GET: /api/listings/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetListing(int id)
    {
        var listing = await _context.Listings.FindAsync(id);
        if (listing == null)
            return NotFound();

        return Ok(listing);
    }

    // POST: /api/listings
    [HttpPost]
    public async Task<IActionResult> CreateListing([FromBody] Listing listing)
    {
        _context.Listings.Add(listing);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetListing), new { id = listing.ListingID }, listing);
    }
    [HttpGet("mystore/{storeId}")]
    public async Task<IActionResult> GetStoreListings(int storeId)
    {
        var listings = await _context.Listings
            .Where(l => l.StoreID == storeId)
            .Include(l => l.RentalStore)
            .ToListAsync();

        return Ok(listings);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateListing(int id, [FromBody] Listing listing)
    {
        if (id != listing.ListingID)
            return BadRequest();

        var existingListing = await _context.Listings.FindAsync(id);
        if (existingListing == null)
            return NotFound();

        _context.Entry(existingListing).CurrentValues.SetValues(listing);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteListing(int id)
    {
        var listing = await _context.Listings.FindAsync(id);
        if (listing == null)
            return NotFound();

        _context.Listings.Remove(listing);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
