using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace wpfsudoku.ValidationRules
{
    public class SudokuCellValidationRule : ValidationRule
    {
        /// <summary>
        /// Validates the proposed cell value.
        /// </summary>
        /// <param name="value">The proposed cell value.</param>
        /// <param name="cultureInfo">A CultureInfo.</param>
        /// <returns>The result of the validation.</returns>
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            if (value == null)
            {
                return ValidationResult.ValidResult;
            }
            if (byte.TryParse(value.ToString(), out byte number) && (number < 1 || number > 9))
            {
                return new ValidationResult(false, null);
            }
            return ValidationResult.ValidResult;
        }
    }
}
