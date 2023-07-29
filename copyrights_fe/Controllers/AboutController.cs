using Microsoft.AspNetCore.Mvc;

namespace copyrights_fe.Controllers
{
    public class AboutController : Controller
    {
        [Route("/about")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
