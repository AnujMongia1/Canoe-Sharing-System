using CanoeSharin.API.Data;
using CanoeSharin.API.Models;
using CanoeSharin.API.DTO_s;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public AuthController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] User user)
    {
        Console.WriteLine($"REGISTER CALLED: {user.Username} / {user.Email}");

        if (_context.Users.Any(u => u.Email == user.Email))
            return BadRequest(new { message = "Email already exists." });

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Registration successful" });
    }


    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto login)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == login.Email && u.Password == login.Password);

        if (user == null)
            return Unauthorized(new { message = "Invalid credentials" });

        return Ok(new
        {
            message = "login successful",
            userId = user.Id,
            username = user.Username
        });
    }

    [HttpPost("register/store")]
    public async Task<IActionResult> RegisterStore([FromBody] RentalStore store)
    {
        if (_context.RentalStores.Any(s => s.Email == store.Email))
            return BadRequest(new { message = "Email already exists." });

        _context.RentalStores.Add(store);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Store registration successful" });
    }

    [HttpPost("login/store")]
    public async Task<IActionResult> LoginStore([FromBody] StoreLoginDto login)
    {
        var store = await _context.RentalStores
            .FirstOrDefaultAsync(s => s.Email == login.Email && s.Password == login.Password);

        if (store == null)
            return Unauthorized(new { message = "Invalid credentials" });

        return Ok(new
        {
            message = "Store login successful",
            storeId = store.StoreID,
            storeName = store.StoreName
        });
    }

}
