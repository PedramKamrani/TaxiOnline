using System.ComponentModel.DataAnnotations;

namespace Snap.Core.ViewModels
{
    public class RegisterViewModel
    {
        [Display(Name = "شماره موبایل")]
        [Required(ErrorMessage = "وارد کردن شماره موبایل الزامیست")]
        [MaxLength(11, ErrorMessage = "مقدار وارد شده بیشتر از حد مجاز است")]
        [MinLength(11, ErrorMessage = "مقدار وارد شده کمتر از حد مجاز است")]
        [Phone(ErrorMessage = "لطفا از فرمت صحیح استفاده نمایید.")]
        public string Username { get; set; }
    }
}
