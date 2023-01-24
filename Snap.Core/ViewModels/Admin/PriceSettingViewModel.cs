using System.ComponentModel.DataAnnotations;

namespace Snap.Core.ViewModels.Admin
{
    public class PriceSettingViewModel
    {
        [Display(Name = "محاسبه آب و هوا در قیمت")]
        public bool IsWeatherPirce { get; set; }

        [Display(Name = "محاسبه بُعد مسافت در قیمت")]
        public bool IsDistance { get; set; }
    }
}
