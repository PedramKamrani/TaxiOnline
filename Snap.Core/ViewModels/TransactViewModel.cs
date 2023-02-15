using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snap.Core.ViewModels
{
    public class TransactViewModel
    {
        public Guid UserId { get; set; }

        public long Fee { get; set; }

        [Display(Name = "آدرس کامل مبدأ")]
        public string StartAddress { get; set; }

        [Display(Name = "طول جغرافیایی مبدأ")]
        public string StartLat { get; set; }

        [Display(Name = "عرض جغرافیایی مبدأ")]
        public string StartLng { get; set; }

        [Display(Name = "آدرس کامل مقصد")]
        public string EndAddress { get; set; }

        [Display(Name = "طول جغرافیایی مقصد")]
        public string EndLat { get; set; }

        [Display(Name = "عرض جغرافیایی مقصد")]
        public string EndLng { get; set; }
    }
}
