using Microsoft.AspNetCore.Mvc;

namespace Snap.Site.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
