using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snap.Core.ViewModels
{
    public class DashboardViewModel
    {
        public int Status { get; set; }

        public Guid? TransactId { get; set; }

        public Guid? DriverId { get; set; }

        public Guid? UserId { get; set; }
    }
}
