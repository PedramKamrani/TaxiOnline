using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snap.Data.Layer.Entities
{
    public class Driver
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid UserId { get; set; }
       
        public Guid? CarId { get; set; }
     
        public Guid? ColorId { get; set; }

        [Display(Name = "کدملی")]
        public string NationalCode { get; set; }
        [Display(Name = "تلفن")]
        public string Telephone { get; set; }
        [Display(Name = "آدرس")]
        public string Address { get; set; }
        [Display(Name = "شماره پلاک")]
        public string Code { get; set; }
        [Display(Name = "عکس")]
        public string Image { get; set; }
        [Display(Name = "پروفایل")]
        public  string Avatar { get; set; }
        [Display(Name = "تایید/عدم تایید")]
        public bool IsConfirm { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        [ForeignKey("ColorId")]
        public  virtual Color? Color { get; set; }
        [ForeignKey("CarId")]
        public virtual Car? Car { get; set; }
    }
}
