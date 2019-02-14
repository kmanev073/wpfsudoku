using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace wpfsudoku.Converters
{
    /// <summary>
    /// Used to bind radio buttons to the difficulties enum.
    /// </summary>
    public class EnumToBoolConverter : IValueConverter
    {
        /// <summary>
        /// Converts the enum value to boolean
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => parameter.Equals(value) ? true : false;

        /// <summary>
        /// Does not convert anything but uses the parameter to set the right enum value
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => parameter;
    }
}
