using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Snap.Core.Interface;
using Snap.Core.Securities;
using Snap.Core.ViewModels.Admin;
using Snap.Data.Layer.Entities;

namespace Snap.Site.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminService _service;

        public AdminController(IAdminService service)
        {
            _service = service;
        }
        public IActionResult Index()
        {
            return View();
        }
        
        #region Setting




        public async Task<IActionResult> SiteSetting()
        {
            Settings setting = await _service.GetSiteSetting();

            SiteSettingViewModel viewModel = new SiteSettingViewModel()
            {
                Desc = setting?.Description ?? "",
                Fax = setting?.Fax ?? "",
                Name = setting?.Name ?? "",
                Tel = setting?.Tel ?? ""
            };

            ViewBag.IsSuccess = false;

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult SiteSetting(SiteSettingViewModel viewModel)
        {

            if (ModelState.IsValid)
            {
                bool result = _service.UpdateSetting(viewModel);

                ViewBag.IsSuccess = result;
            }

            return View(viewModel);
        }

        public async Task<IActionResult> PriceSetting()
        {
            Settings setting = await _service.GetSiteSetting();

            PriceSettingViewModel viewModel = new PriceSettingViewModel()
            {
                IsDistance = setting.IsDistance,
                IsWeatherPirce = setting.IsWeatherPrice
            };

            ViewBag.IsSuccess = false;

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult PriceSetting(PriceSettingViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                bool result = _service.UpdatePriceSetting(viewModel);

                ViewBag.IsSuccess = result;
            }

            return View(viewModel);
        }

        public async Task<IActionResult> AboutSetting()
        {
            Settings setting = await _service.GetSiteSetting();

            AboutSettingViewModel viewModel = new AboutSettingViewModel()
            {
                About = setting.About
            };

            ViewBag.IsSuccess = false;

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult AboutSetting(AboutSettingViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                bool result = _service.UpdateAboutSetting(viewModel);

                ViewBag.IsSuccess = result;
            }

            return View(viewModel);
        }

        public async Task<IActionResult> TermsSetting()
        {
            Settings setting = await _service.GetSiteSetting();

            TermsSettingViewModel viewModel = new TermsSettingViewModel()
            {
                Terms = setting.Trems
            };

            ViewBag.IsSuccess = false;

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult TermsSetting(TermsSettingViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                bool result = _service.UpdateTermsSetting(viewModel);

                ViewBag.IsSuccess = result;
            }

            return View(viewModel);
        }

        #endregion
    }
}
