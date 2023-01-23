using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Snap.Data.Layer.Entities
{
    public class Color
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }
        [Display(Name = "رنگ")]
        public string Name { get; set; }

        public virtual ICollection<Driver>Drivers { get; set; }
    }
}
