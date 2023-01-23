using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snap.Data.Layer.Entities
{
    public class UserDetail
    {
        [Key]
        [ForeignKey("User")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid UserId { get; set; }
        [Display(Name = "تاریخ عضویت")]
        public string Date { get; set; }
        [Display(Name = "زمان عضویت")]
        public string Time { get; set; }
        [Display(Name = "نام و نام خانوادگی")]
        public string Fullname { get; set; }
        [Display(Name = "تاریخ تولد")]
        public string BirthDate { get; set; }
        public virtual User User { get; set; }
        
    }
}
