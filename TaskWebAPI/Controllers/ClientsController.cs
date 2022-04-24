using Microsoft.AspNetCore.Mvc;

namespace TaskWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
