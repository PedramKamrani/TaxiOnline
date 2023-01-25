using System.ComponentModel.DataAnnotations;

namespace Snap.Core.ViewModels.Admin
{
    public class MonthTypeViewModel
    {
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "نباید بدون مقدار باشد")]
        [MaxLength(100, ErrorMessage = "مقدار {0} نباید بیش تر از {1} کاراکتر باشد")]
        public string Name { get; set; }

        [Display(Name = "از")]
        public int Start { get; set; }

        [Display(Name = "تا")]
        public int End { get; set; }

        [Display(Name = "درصد")]
        public float Percent { get; set; }
    }
}
