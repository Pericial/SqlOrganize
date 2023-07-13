using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public static class DateTimeUtils
    {
        public static int ToSemester(this DateTime date )
        {
            return (Int32.Parse(date.ToString("MM")) < 6) ? 1 : 2;
        }

        public static string ToYearSemester(this DateTime alta)
        {
            return alta.ToString("yyyy") + alta.ToSemester().ToString();
        }
    }
}
