using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snap.Core.Generators
{
    public static class MonthConvertor
    {
        public static string FarsiMonth(int id)
        {
            string strMonth = "";

            switch (id)
            {
                case 1:
                    strMonth = "فروردین";
                    break;
                case 2:
                    strMonth = "اردیبهشت";
                    break;
                case 3:
                    strMonth = "خرداد";
                    break;
                case 4:
                    strMonth = "تیر";
                    break;
                case 5:
                    strMonth = "مرداد";
                    break;
                case 6:
                    strMonth = "شهریور";
                    break;
                case 7:
                    strMonth = "مهر";
                    break;
                case 8:
                    strMonth = "آبان";
                    break;
                case 9:
                    strMonth = "آذر";
                    break;
                case 10:
                    strMonth = "دی";
                    break;
                case 11:
                    strMonth = "بهمن";
                    break;
                case 12:
                    strMonth = "اسفند";
                    break;
                default:
                    break;
            }

            return strMonth;
        }
    }
}
