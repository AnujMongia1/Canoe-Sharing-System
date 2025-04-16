using CanoeSharin.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CanoeSharin.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CanoesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public CanoesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /api/canoes
        [HttpGet]
        public async Task<IActionResult> GetCanoes()
        {
            var canoes = await _context.Canoes.ToListAsync();
            return Ok(canoes);
        }
    }

}
