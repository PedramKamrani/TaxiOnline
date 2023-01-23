using System.ComponentModel.DataAnnotations;

namespace Snap.Core.ViewModels.Admin
{
    public class CarViewModel
    {
        [Display(Name = "نام خودرو")]
        [Required(ErrorMessage = "وارد نمود  اجباریست")]
        [MinLength(2, ErrorMessage = "لطفا {0}نباید کمتر از {1}باشد.")]
        public string Name { get; set; }
        public Guid CarId { get; set; }
    }
}
