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
    public class EnumToStringConverter : IValueConverter
    {
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

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => parameter;
    }
}
