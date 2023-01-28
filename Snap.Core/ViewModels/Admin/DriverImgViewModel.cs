using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Snap.Core.ViewModels.Admin
{
    public class DriverImgViewModel
    {
        [Display(Name = "تصویر گواهینامه")]
        public IFormFile Img { get; set; }

        public string ImgName { get; set; } = "";

        [Display(Name = "تأیید")]
        public bool IsConfirm { get; set; }
    }
}
