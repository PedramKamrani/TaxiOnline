using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Snap.Core.Generators;
using Snap.Core.Interface;
using Snap.Core.Securities;
using Snap.Core.ViewModels;
using Snap.Core.ViewModels.Admin;
using Snap.Data.Layer.Entities;
using System.Globalization;

namespace Snap.Site.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminService _service;
        private PersianCalendar pc = new PersianCalendar();
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

        #region Chart

        public async Task<IActionResult> WeeklyFactor()
        {
            var strToday = DateTimeGenerators.ShamsiDate();
            int Ayear = Convert.ToInt32(strToday.Substring(0, 4));
            int Amonth = Convert.ToInt32(strToday.Substring(5, 2));
            int Aday = Convert.ToInt32(strToday.Substring(8, 2));
            var strEndDate = "";
            var charts = new List<ChartViewModel>();

            int intM = 0;

            for (int i = 0; i <=6; i++)
            {
                DateTime dtA = pc.ToDateTime(Ayear, Amonth, Aday, 0, 0, 0, 0);

                if (i == 0)
                {
                    dtA = dtA.AddDays(i);
                }
                else
                {
                    intM = -i;

                    dtA = dtA.AddDays(intM);
                }
                strEndDate = pc.GetYear(dtA).ToString("0000") + "/" + pc.GetMonth(dtA).ToString("00")
                             + "/" + pc.GetDayOfMonth(dtA).ToString("00");

                ChartViewModel chart = new ChartViewModel()
                {
                    Label = strEndDate,
                    Value =await _service.WeeklyFactor(strEndDate),
                    Color = ColorGenerators.SelectColorCode()
                };

                charts.Add(chart);
            }
            return View(charts);
        }

        public IActionResult MonthlyFactor()
        {
            var charts = new List<ChartViewModel>();

            for (int i = 1; i <= 12; i++)
            {
                string strMonth = i.ToString("00");

                ChartViewModel chart = new ChartViewModel()
                {
                    Color = ColorGenerators.SelectColorCode(),
                    Label = MonthConvertor.FarsiMonth(i),
                    Value = _service.MonthlyFactor(strMonth)
                };

                charts.Add(chart);
            }

            return View(charts);
        }


        public IActionResult WeeklyRegister()
        {
            // 0000/00/00
            string strToday = DateTimeGenerators.ShamsiDate();

            int Ayear = Convert.ToInt32(strToday.Substring(0, 4));
            int Amonth = Convert.ToInt32(strToday.Substring(5, 2));
            int Aday = Convert.ToInt32(strToday.Substring(8, 2));

            string strEndDate = "";

            var charts = new List<ChartViewModel>();

            int intM = 0;

            for (int i = 0; i <= 6; i++)
            {
                DateTime dtA = pc.ToDateTime(Ayear, Amonth, Aday, 0, 0, 0, 0);

                if (i == 0)
                {
                    dtA = dtA.AddDays(i);
                }
                else
                {
                    intM = -i;

                    dtA = dtA.AddDays(intM);
                }

                strEndDate = pc.GetYear(dtA).ToString("0000") + "/" + pc.GetMonth(dtA).ToString("00")
                             + "/" + pc.GetDayOfMonth(dtA).ToString("00");

                ChartViewModel chart = new ChartViewModel()
                {
                    Label = strEndDate,
                    Value = _service.WeeklyRegister(strEndDate),
                    Color = ColorGenerators.SelectColorCode()
                };

                charts.Add(chart);
            }

            return View(charts);
        }

        public IActionResult MonthlyRegister()
        {
            var charts = new List<ChartViewModel>();

            for (int i = 1; i <= 12; i++)
            {
                string strMonth = i.ToString("00");

                ChartViewModel chart = new ChartViewModel()
                {
                    Color = ColorGenerators.SelectColorCode(),
                    Label = MonthConvertor.FarsiMonth(i),
                    Value = _service.MonthlyRegister(strMonth)
                };

                charts.Add(chart);
            }

            return View(charts);
        }

        #endregion
    }
}
