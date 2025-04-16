using CanoeSharingSystemWebAPI.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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
            var Listings = await _context.Listings.Include(x => x.Bookings)
                .ThenInclude(x => x.Reviews)
                .ToListAsync();

            return Ok(Listings);
        }

        //[Authorize(Policy = "UserOrStoreAccess")]
        //Route: GET CanoeSharingSystemApi/Listings/{id}
        [HttpGet("Listings/{id}")]
        public async Task<IActionResult> GetAllListingsById(int id)
        {
            var Listing = await _context.Listings.FindAsync(id);
            return Ok(Listing);
        }

        //[Authorize(Policy = "UserOrStoreAccess")]
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

        //Route: POST CanoeSharingSystemApi/Users/Register
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

        [HttpPost("Users/Login")]
        public async Task<IActionResult> LoginAsUser([FromBody] User user)
        {

            if(user == null)
            {
                return BadRequest();
            }

            var userfromdb = await _context.Users.FirstOrDefaultAsync(x => x.Email == user.Email && x.Password==user.Password);
            if (user==null)
            {
                return Unauthorized("Invalid user credentials");

            }
            var token = GenerateJwtToken(userfromdb.Username, "user");
            return Ok(token);

        }

        [HttpPost("Stores/Login")]
        public async Task<IActionResult> LoginAsStore([FromBody] RentalStore rentalStore)
        {

            if (rentalStore == null)
            {
                return BadRequest();
            }

            var userfromdb = await _context.RentalStores.FirstOrDefaultAsync(x => x.Email == rentalStore.Email && x.Password==rentalStore.Password);
            if (rentalStore==null)
            {
                return Unauthorized("Invalid store credentials");

            }
            var token = GenerateJwtToken(userfromdb.StoreName, "store");
            return Ok(token);

        }

        //Route: POST CanoeSharingSystemApi/RentalStore/Register
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

        //Route: GET CanoeSharingSystemApi/Bookings
        [HttpGet("Bookings")]
        public async Task<IActionResult> GetAllBookings()
        {
            var Bookings = await _context.Bookings.ToListAsync();
            return Ok(Bookings);
        }

        //Route: GET CanoeSharingSystemApi/Bookings/{id}
        [HttpGet("Bookings/{id}")]
        public async Task<IActionResult> GetAllBookingsById(int id)
        {
            var Booking = await _context.Bookings.FindAsync(id);
            return Ok(Booking);
        }

        //[Authorize(Policy = "UserOnlyAccess, StoreOnlyAccess")]
        //Route: POST CanoeSharingSystemApi/Bookings/MakeBooking
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

        //Route: POST CanoeSharingSystemApi/Bookings/MakeBooking
        [HttpPost("Reviews/AddReview")]
        public async Task<IActionResult> AddReview([FromBody] Booking booking)
        {
            if (booking == null)
            {
                return BadRequest();
            }
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();
            return Ok();
        }

        //https://medium.com/@solomongetachew112/jwt-authentication-in-net-8-a-complete-guide-for-secure-and-scalable-applications-6281e5e8667c
        private string GenerateJwtToken(string username, string role)
        {
            var claims = new[]
            {
            new Claim("username", username),
            new Claim("role", role)
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("lnvcayikectkthacfjvdceaavmnuyjoneipkzntxpnavoxzlzbxgwsvxrbhculzzebwynwhmqkwtakgsbgpqlzaqjbsozccgedanipbpgwbfispdybkdavjayharbfrm"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "CanoeSharingSystemAPI",
                claims: claims,
                expires: DateTime.Now.AddMinutes(300),
                
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
