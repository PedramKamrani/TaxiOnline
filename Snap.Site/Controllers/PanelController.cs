using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Snap.Site.Controllers
{
    [Authorize]
    public class PanelController : Controller
    {
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
