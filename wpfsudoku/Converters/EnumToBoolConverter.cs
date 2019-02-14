using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace wpfsudoku.Converters
{
    public class EnumToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => parameter.Equals(value) ? true : false;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => parameter;
    }
}
