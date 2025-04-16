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
            .Include(l => l.User)
            .Include(l => l.RentalStore)
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
}
