using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Snap.Data.Layer.Entities
{
    public class Discount
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        [Display(Name = "عنوان")]
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Display(Name = "کد تخفیف")]
        [Required]
        [MaxLength(10)]
        public string Code { get; set; }

        [Display(Name = "مبلغ تخفیف")]
        public long Price { get; set; }

        [Display(Name = "درصد تخفیف")]
        public int Percent { get; set; }

        [Display(Name = "سایر توضیحات")]
        public string Desc { get; set; }

        [Display(Name = "تاریخ شروع")]
        [MaxLength(10)]
        public string Start { get; set; }

        [Display(Name = "تاریخ پایان")]
        [MaxLength(10)]
        public string Expire { get; set; }
    }
}
