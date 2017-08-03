using System.Globalization;
using System.Windows.Controls;

namespace CsenPoomsaeScore
{
    public class NotEmptyStringRule : ValidationRule
    {
        public NotEmptyStringRule()
        {
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (((string)value).Length > 0)
            {
                return new ValidationResult(true, null);
            }
            else
            {
                return new ValidationResult(false, "Casella vuota.");
            }
        }
    }
}
