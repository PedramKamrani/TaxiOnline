using Microsoft.AspNetCore.Mvc;
using Snap.Core.Interface;
using Snap.Core.ViewModels.Panel;
using Snap.Data.Layer.Entities;

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
            User user = _panel.GetUser(User.Identity.Name);

            Transact transact = _panel.GetDriverConfrimTransact(user.Id);

            if (transact == null)
            {
                DriverPanelViewModel viewModel = new DriverPanelViewModel()
                {
                    DriverId = user.Id,
                    Status = 0,
                    Desc = "",
                    EndLat = "",
                    EndLng = "",
                    Price = 0,
                    StartLat = "",
                    StartLng = "",
                    TransactId = null,
                    UserId = null,
                    UserName = ""
                };

                return View(viewModel);
            }
            else
            {
                string username = _panel.GetUserById(transact.UserId).UserDetail.Fullname;

                DriverPanelViewModel viewModel = new DriverPanelViewModel()
                {
                    DriverId = user.Id,
                    Status = transact.Status,
                    Desc = "",
                    EndLat = transact.EndLat,
                    EndLng = transact.EndLng,
                    Price = transact.Total,
                    StartLat = transact.StartLat,
                    StartLng = transact.StartLng,
                    TransactId = transact.Id,
                    UserId = transact.UserId,
                    UserName = username
                };

                return View(viewModel);
            }
        }

        public JsonResult List()
        {
            var result = _panel.GetTransactsNotAccept();

            return new JsonResult(result);
        }

        public IActionResult UpdateStatus(Guid id,int status)
        {
            _panel.UpdateStatus(id, 1);

            return RedirectToAction("Index");
        }

        public IActionResult UpdateStatus(Guid id)
        {
            User user = _panel.GetUser(User.Identity.Name);

            _panel.UpdateStatus(id, user.Id, 1);

            return RedirectToAction("Index");
        }

        public IActionResult EndRequest(Guid id)
        {
            _panel.EndRequest(id);

            return RedirectToAction("Index");
        }
    }
}
