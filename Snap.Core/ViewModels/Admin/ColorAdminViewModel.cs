using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snap.Core.ViewModels.Admin
{
    public class ColorAdminViewModel
    {
        [Display(Name = "ایدی")]
        public Guid Id { get; set; }
        [Display(Name = "رنگ")]
        public string Name { get; set; }

        [Display(Name = "کدرنگ")]
        public string Code { get; set; }
    }
}
