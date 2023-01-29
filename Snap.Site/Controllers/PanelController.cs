using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Snap.Core.Generators;
using Snap.Core.Interface;
using Snap.Core.ViewModels;

namespace Snap.Site.Controllers
{
    [Authorize]
    public class PanelController : Controller
    {
        private IPanelService _panel;

        public PanelController(IPanelService panel)
        {
            _panel = panel;
        }
        public IActionResult Dashboard()
        {
            return View();
        }

        public async Task<IActionResult> UserProfile()
        {
            var result = await _panel.GetUserDetailsAsync(User.Identity.Name);

            UserDetailProfileViewModel viewModel = new UserDetailProfileViewModel()
            {
                BirthDate = result.BirthDate,
                FullName = result.Fullname
            };

            ViewBag.IsSuccess = false;
            ViewBag.MyDate = DateTimeGenerators.ShamsiDate();

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> UserProfile(UserDetailProfileViewModel viewModel)
        {
            var result = await _panel.GetUserDetailsAsync(User.Identity.Name);

            bool myUpdate = _panel.UpdateUserDetailsProfile(result.UserId, viewModel);

            ViewBag.MyDate = viewModel.BirthDate;
            ViewBag.IsSuccess = myUpdate;
            return View(viewModel);
        }

    }
}
