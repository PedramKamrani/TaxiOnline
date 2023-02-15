using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snap.Data.Layer.Entities
{
    public class Transact
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        [Display(Name = "تاریخ")]
        [MaxLength(10)]
        public string Date { get; set; }

        [Display(Name = "زمان تأیید سفر")]
        [MaxLength(10)]
        public string StartTime { get; set; }

        [Display(Name = "زمان رسیدن به مقصد")]
        [MaxLength(10)]
        public string EndTime { get; set; }

        [Display(Name = "راننده")]
        public Guid? DriverId { get; set; }

        [Display(Name = "مسافر")]
        public Guid UserId { get; set; }

        [Display(Name = "کرایه")]
        public long Fee { get; set; }

        [Display(Name = "تخفیف")]
        public long Discount { get; set; }

        [Display(Name = "جمع کل")]
        public long Total { get; set; }

        [Display(Name = "نقد")]
        public bool IsCash { get; set; }

        [Display(Name = "آدرس کامل مبدأ")]
        public string StartAddress { get; set; }

        [Display(Name = "طول جغرافیایی مبدأ")]
        [MaxLength(20)]
        public string StartLat { get; set; }

        [Display(Name = "عرض جغرافیایی مبدأ")]
        [MaxLength(20)]
        public string StartLng { get; set; }

        [Display(Name = "آدرس کامل مقصد")]
        public string EndAddress { get; set; }

        [Display(Name = "طول جغرافیایی مقصد")]
        [MaxLength(20)]
        public string EndLat { get; set; }

        [Display(Name = "عرض جغرافیایی مقصد")]
        [MaxLength(20)]
        public string EndLng { get; set; }

        [Display(Name = "امتیاز")]
        public int Rate { get; set; }

        [Display(Name = "رضایت از سفر")]
        public bool DriverRate { get; set; }

        [Display(Name = "وضعیت")]

        // 0 == create   1 == updatedriver  2 == success  3 == cancel
        public int Status { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public virtual ICollection<TransactRate> TransactRates { get; set; }

    }
}
