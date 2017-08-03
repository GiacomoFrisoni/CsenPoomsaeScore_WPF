using System;
using System.Globalization;
using System.Windows.Controls;

namespace CsenPoomsaeScore
{
    class OnlyNumberStringRule : ValidationRule
    {
        public OnlyNumberStringRule()
        {
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            double tmp;
            if (String.IsNullOrEmpty((String)value))
                return new ValidationResult(false, "Casella vuota.");
            if (Double.TryParse((String)value, out tmp))
                return new ValidationResult(true, null);
            else
                return new ValidationResult(false, "Valore non valido.");
        }
    }
}
