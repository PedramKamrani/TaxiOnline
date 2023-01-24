using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snap.Core.ViewModels.Admin
{
    public class RateTypeViewModel
    {
        [Display(Name = "ایدی")]
        public Guid Id { get; set; }
        [Display(Name = "امتیاز")]
        [MaxLength(40,ErrorMessage = "نباید بیشتراز {1}باشد")]
        public string Name { get; set; }
        [Display(Name = "مثبت")]
        public bool OK { get; set; }
        [Display(Name = "ترتیب")]
        public int OrderView { get; set; }
    }
}
