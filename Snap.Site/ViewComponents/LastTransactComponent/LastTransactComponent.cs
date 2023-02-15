using Microsoft.AspNetCore.Mvc;
using Snap.Core.Interface;

namespace Snap.Site.ViewComponents.LastTransactComponent
{
    public class LastTransactComponent : ViewComponent
    {
        private readonly IAdminService _admin;

        public LastTransactComponent(IAdminService admin)
        {
            _admin = admin;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult)View("ViewLastTransact", _admin.LastTransact()));
        }
    }
}
