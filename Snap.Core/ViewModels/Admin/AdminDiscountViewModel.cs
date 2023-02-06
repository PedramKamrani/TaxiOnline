using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snap.Core.ViewModels.Admin
{
    public class AdminDiscountViewModel
    {
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "نباید بدون مقدار باشد")]
        [MaxLength(100, ErrorMessage = "مقدار {0} نباید بیش تر از {1} کاراکتر باشد")]
        public string Name { get; set; }

        [Display(Name = "کد تخفیف")]
        [Required(ErrorMessage = "نباید بدون مقدار باشد")]
        [MaxLength(10, ErrorMessage = "مقدار {0} نباید بیش تر از {1} کاراکتر باشد")]
        public string Code { get; set; }

        [Display(Name = "مبلغ تخفیف")]
        public long Price { get; set; }

        [Display(Name = "درصد تخفیف")]
        public int Percent { get; set; }

        [Display(Name = "سایر توضیحات")]
        [DataType(DataType.MultilineText)]
        public string Desc { get; set; }

        [Display(Name = "تاریخ شروع")]
        [MaxLength(10)]
        public string Start { get; set; }

        [Display(Name = "تاریخ پایان")]
        [MaxLength(10)]
        public string Expire { get; set; }
    }
}
