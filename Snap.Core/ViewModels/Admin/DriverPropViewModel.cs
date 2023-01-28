using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Snap.Core.ViewModels.Admin
{
    public class DriverPropViewModel
    {
        [Display(Name = "کد ملی")]
        [Required(ErrorMessage = "نباید بدون مقدار باشد")]
        [Phone(ErrorMessage = "فقط عدد می توانید وارد کنید")]
        [MaxLength(10, ErrorMessage = "مقدار {0} نباید بیش تر از {1} کاراکتر باشد")]
        public string NationalCode { get; set; }

        [Display(Name = "شماره ثابت")]
        [MaxLength(30, ErrorMessage = "مقدار {0} نباید بیش تر از {1} کاراکتر باشد")]
        public string Tel { get; set; }

        [Display(Name = "آدرس")]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }

        [Display(Name = "تصویر")]
        public IFormFile Avatar { get; set; }

        public string AvatarName { get; set; } = "";
    }
}
