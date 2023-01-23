using Microsoft.AspNetCore.Mvc;

namespace Snap.Site.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
