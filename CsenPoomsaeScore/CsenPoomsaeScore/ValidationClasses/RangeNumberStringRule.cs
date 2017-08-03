using System;
using System.Globalization;
using System.Windows.Controls;

namespace CsenPoomsaeScore
{
    class RangeNumberStringRule : ValidationRule
    {
        private double _min;
        private double _max;

        public RangeNumberStringRule()
        {
        }

        public double Min
        {
            get { return _min; }
            set { _min = value; }
        }

        public double Max
        {
            get { return _max; }
            set { _max = value; }
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            double tmp;
            String str_value = ((String)value).Replace(',', '.');
            if (String.IsNullOrEmpty(str_value))
                return new ValidationResult(false, "Casella vuota.");
            if (Double.TryParse(str_value, NumberStyles.Any, CultureInfo.InvariantCulture, out tmp))
            {
                if ((tmp < Min) || (tmp > Max))
                {
                    return new ValidationResult(false,
                      "Inserire un valore nel range: " + Min + " - " + Max + ".");
                }
                else
                    return new ValidationResult(true, null);
            }
            else
                return new ValidationResult(false, "Valore non valido.");
        }
    }
}
