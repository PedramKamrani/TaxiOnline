using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snap.Data.Layer.Entities
{
    public class TransactRate
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        public Guid TransactId { get; set; }

        public Guid RateTypeId { get; set; }

        [ForeignKey("TransactId")]
        public virtual Transact Transact { get; set; }

        [ForeignKey("RateTypeId")]
        public virtual RateType RateType { get; set; }
    }
}
