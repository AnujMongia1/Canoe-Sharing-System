using CanoeSharingSystemWebAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CanoeSharingSystemWebAPI.Controllers
{
    [ApiController]
    [Route("CanoeSharingSystemApi")]
    public class CanoeSharingAPIController : ControllerBase
    {
        private readonly CanoeSharingAPIDbContext _context;

        public CanoeSharingAPIController(CanoeSharingAPIDbContext context)
        {
            _context=context;
        }

        //Route: GET CanoeSharingSystemApi/Listings 
        [HttpGet("Listings")]
        public async Task<IActionResult> GetAllListings()
        {
            var Listings = await _context.Listings.ToListAsync();
            return Ok(Listings);
        }

        //Route: GET CanoeSharingSystemApi/Listings/{id}
        [HttpGet("Listings/{id}")]
        public async Task<IActionResult> GetAllListingsById(int id)
        {
            var Listing = await _context.Listings.FindAsync(id);
            return Ok(Listing);
        }

        //Route: POST CanoeSharingSystemApi/AddListing
        [HttpPost("AddListing")]
        public async Task<IActionResult> AddCanoeListing([FromBody] Listing Listing)
        {
            if (Listing == null)
            {
                return BadRequest("No data sent");
            }

            _context.Listings.Add(Listing);
            await _context.SaveChangesAsync();
            return Ok();
        }

        //Route: POST CanoeSharingSystemApi/AddListing
        [HttpPost("Users/Register")]
        public async Task<IActionResult> RegisterAsUser([FromBody] User User)
        {
            if (User == null)
            {
                return BadRequest("No data sent");
            }

            _context.Users.Add(User);
            await _context.SaveChangesAsync();
            return Ok();
        }

        //Route: POST CanoeSharingSystemApi/AddListing
        [HttpPost("RentalStore/Register")]
        public async Task<IActionResult> RegisterAsStore([FromBody] RentalStore rentalStore)
        {
            if (rentalStore == null)
            {
                return BadRequest("No data sent");
            }

            _context.RentalStores.Add(rentalStore);
            await _context.SaveChangesAsync();
            return Ok();
        }

        //Route: GET CanoeSharingSystemApi/Listings 
        [HttpGet("Bookings")]
        public async Task<IActionResult> GetAllBookings()
        {
            var Bookings = await _context.Bookings.ToListAsync();
            return Ok(Bookings);
        }

        //Route: GET CanoeSharingSystemApi/Listings/{id}
        [HttpGet("Bookings/{id}")]
        public async Task<IActionResult> GetAllBookingsById(int id)
        {
            var Booking = await _context.Bookings.FindAsync(id);
            return Ok(Booking);
        }

        [HttpPost("Bookings/MakeBooking")]
        public async Task<IActionResult> MakeBooking([FromBody] Booking booking)
        {
            if(booking == null)
            {
                return BadRequest();
            }
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();
            return Ok();
        }



    }
}
