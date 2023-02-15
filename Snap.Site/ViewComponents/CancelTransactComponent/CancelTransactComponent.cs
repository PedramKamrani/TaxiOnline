using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using Snap.Core.Interface;

namespace Snap.Site.ViewComponents.CancelTransacComponent
{
    public class CancelTransacComponent : ViewComponent
    {
        private readonly IAdminService _admin;
        private PersianCalendar pc = new PersianCalendar();

        public CancelTransacComponent(IAdminService admin)
        {
            _admin = admin;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            string strToday = pc.GetYear(DateTime.Now).ToString("0000") + "/" +
                pc.GetMonth(DateTime.Now).ToString("00") + "/" + pc.GetDayOfMonth(DateTime.Now).ToString("00");

            return await Task.FromResult((IViewComponentResult)View("ViewCancelTransact", _admin.FillCancelTransact(strToday)));
        }
    }
}
