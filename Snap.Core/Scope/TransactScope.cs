using Snap.Data.Layer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Snap.Core.Interface;

namespace Snap.Core.Scope
{
    public class TransactScope
    {
        private readonly IAdminService _admin;

        public TransactScope(IAdminService admin)
        {
            _admin = admin;
        }

        public User GetUserById(Guid id)
        {
            return _admin.GetUserById(id).Result;
        }
    }

}
