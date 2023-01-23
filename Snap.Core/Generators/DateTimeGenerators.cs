using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snap.Core.Generators
{
    public static class DateTimeGenerators
    {
        public static string ShamsiDate()
        {
            PersianCalendar pc=new PersianCalendar();
            return pc.GetYear(DateTime.Now).ToString("0000") + "/"
                                                             + pc.GetMonth(DateTime.Now).ToString("00") + "/"
                                                             + pc.GetDayOfMonth(DateTime.Now);
        }

        public static string ShamsiTime()
        {
            PersianCalendar  pc=new PersianCalendar();
            return pc.GetHour(DateTime.Now).ToString("00") + ":"
                                                           + pc.GetMinute(DateTime.Now).ToString("00") + ":"
                                                           + pc.GetSecond(DateTime.Now);
        }
    }
}
