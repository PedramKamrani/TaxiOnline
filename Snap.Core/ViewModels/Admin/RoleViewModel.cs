using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snap.Core.ViewModels.Admin
{
    public class RoleViewModel
    {
        public Guid Id { get; set; }
        [Display(Name = "عنوان سیستمی نقش")]
        [Required(ErrorMessage = "نباید بدون مقدار باشد")]
        [MaxLength(30, ErrorMessage = "مقدار {0} نباید بیش تر از {1} کاراکتر باشد")]
        public string Name { get; set; }

        [Display(Name = "عنوان نمایشی نقش")]
        [Required(ErrorMessage = "نباید بدون مقدار باشد")]
        [MaxLength(30, ErrorMessage = "مقدار {0} نباید بیش تر از {1} کاراکتر باشد")]
        public string Title { get; set; }
    }
}
