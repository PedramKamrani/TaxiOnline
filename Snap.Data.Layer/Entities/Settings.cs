using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snap.Data.Layer.Entities
{
    public class Settings
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }
        [Display(Name = "تنظیمات")]
        public string Name { get; set; }

        public string Description { get; set; }
        public string About { get; set; }
        public string Trems { get; set; }
        public string Tel{ get; set; }
        public string Fax { get; set; }
        public bool IsWeatherPrice { get; set; }
        public bool IsDistance { get; set; }
    }
}
