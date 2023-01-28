using System.ComponentModel.DataAnnotations;

namespace Snap.Core.ViewModels.Admin
{
    public class DriverCarViewModel
    {
        [Display(Name = "انتخاب خودرو")]
        public Guid? CarId { get; set; }

        [Display(Name = "انتخاب رنگ")]
        public Guid? ColorId { get; set; }

        [Display(Name = "شماره پلاک")]
        [Required(ErrorMessage = "نباید بدون مقدار باشد")]
        public string? CarCode { get; set; }
    }
}
