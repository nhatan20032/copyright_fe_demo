using Microsoft.AspNetCore.Mvc;

namespace copyrights_fe.Controllers
{
    public class ContactController : Controller
    {
        [Route("/contact")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
