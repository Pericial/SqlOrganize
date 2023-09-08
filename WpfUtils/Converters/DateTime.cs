using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WpfUtils.Converters
{

    /// <summary>
    /// Converter para visualizar correctamente las fechas null en una grilla
    /// </summary>
    /// <remarks>https://stackoverflow.com/questions/28080010/display-a-null-or-default-datetime-value-as-blank-or-na-in-a-wpf-datagrid</remarks>
    public class DateTimeNullConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value is DateTime && (DateTime)value < new DateTime(2, 1, 1))
            {
                return null;
            }
            else
                return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
