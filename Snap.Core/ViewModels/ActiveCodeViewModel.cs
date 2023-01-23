using System.ComponentModel.DataAnnotations;

namespace Snap.Core.ViewModels
{
    public class ActiveCodeViewModel
    {
        [Display(Name = "کدفعال سازی")]
        [Required(ErrorMessage = "اواردکردن این کدالزامیست")]
        [MaxLength(6,ErrorMessage = "مقدارنباید بیشتراز6 باشد")]
        [MinLength(6,ErrorMessage = "مقدارنباید کمتراز6 باشد")]
        public string Code { get; set; }
        public string UserName { get; set; }
    }
}
