using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using Snap.Core.Generators;
using Snap.Core.Interface;
using Snap.Core.ViewModels;

namespace Snapp.Site.ViewComponents.WeeklyTransactComponent
{
    public class WeeklyTransactComponent : ViewComponent
    {
        private readonly IAdminService _admin;
        private PersianCalendar pc = new PersianCalendar();

        public WeeklyTransactComponent(IAdminService admin)
        {
            _admin = admin;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            // 0000/00/00
            string strToday = DateTimeGenerators.ShamsiDate();

            int Ayear = Convert.ToInt32(strToday.Substring(0, 4));
            int Amonth = Convert.ToInt32(strToday.Substring(5, 2));
            int Aday = Convert.ToInt32(strToday.Substring(8, 2));

            string strEndDate = "";

            var charts = new List<ChartViewModel>();

            int intM = 0;

            for (int i = 0; i <= 6; i++)
            {
                DateTime dtA = pc.ToDateTime(Ayear, Amonth, Aday, 0, 0, 0, 0);

                if (i == 0)
                {
                    dtA = dtA.AddDays(i);
                }
                else
                {
                    intM = -i;

                    dtA = dtA.AddDays(intM);
                }

                strEndDate = pc.GetYear(dtA).ToString("0000") + "/" + pc.GetMonth(dtA).ToString("00")
                     + "/" + pc.GetDayOfMonth(dtA).ToString("00");

                ChartViewModel chart = new ChartViewModel()
                {
                    Label = strEndDate,
                    Value = _admin.WeeklyTransact(strEndDate),
                    Color = ColorGenerators.SelectColorCode()
                };

                charts.Add(chart);
            }

            return await Task.FromResult((IViewComponentResult)View("ViewWeeklyTransact", charts));
        }
    }
}
