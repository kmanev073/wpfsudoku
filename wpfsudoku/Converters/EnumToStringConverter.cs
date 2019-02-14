using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using wpfsudokulib.Enums;

namespace wpfsudoku.Converters
{
    /// <summary>
    /// Used to translate the game status enum to string.
    /// </summary>
    public class EnumToStringConverter : IValueConverter
    {
        /// <summary>
        /// Converts the enum value to string
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var result = "";
            switch ((GameStatuses)value)
            {
                case GameStatuses.NotStarted:
                    result = "Good luck!";
                    break;
                case GameStatuses.Playing:
                    result = "Keep going...";
                    break;
                case GameStatuses.Finished:
                    result = "Congratulations!";
                    break;
            }
            return result;
        }

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
