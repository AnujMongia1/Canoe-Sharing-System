using Microsoft.AspNetCore.Mvc;

namespace CanoeSharingSystemWebAPI.Controllers
{
    public class CanoeSharingAPIController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
