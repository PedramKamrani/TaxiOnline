using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Snap.Core.Interface;

namespace Snap.Core.Scope
{
    public class SiteScopeLayout
    {
        private readonly IPanelService _service;

        public SiteScopeLayout(IPanelService service)
        {
            _service = service;
        }

        public string GetRoleName(string userName)
        {
            return _service.GetRoleName(userName);
        }
    }
}
