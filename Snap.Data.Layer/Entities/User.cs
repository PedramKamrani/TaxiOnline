using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Snap.Data.Layer.Entities
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public  Guid Id { get; set; }
        [Display(Name = "انتخاب نقش")]
        public  Guid RoleId { get; set; }
        [Display(Name = "نام کاربری")]
        [MaxLength(11)]
        public string UserName { get; set; }
        [Display(Name = "کلمه ورود")]
        [MaxLength(100)]
        public string Password { get; set; }
        [Display(Name = "توکن")]
        public string Token { get; set; }

        [Display(Name = "کیف پول")]
        public long Wallet { get; set; }
        [Display(Name = "فعال/غیرفعال")]
        public bool IsActive { get; set; }
        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }

        public virtual Driver Driver { get; set; }
        public virtual UserDetail UserDetail { get; set; }
    }
}
