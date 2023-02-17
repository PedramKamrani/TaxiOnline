using Microsoft.AspNetCore.Mvc;
using Snap.Core.Interface;

namespace Snap.Site.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriverPanelController : ControllerBase
    {
        private IPanelService _panel;

        public DriverPanelController(IPanelService panel)
        {
            _panel = panel;
        }

        public IActionResult Index()
        {
            var result = _panel.GetTransactsNotAccept();
            return View(result);
        }

        public IActionResult UpdateStatus(Guid id)
        {
            _panel.UpdateStatus(id, 1);

            return RedirectToAction("Index");
        }
    }
}
