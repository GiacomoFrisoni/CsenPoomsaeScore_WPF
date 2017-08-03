using System;
using System.Windows.Data;
using System.Windows.Media;

namespace CsenPoomsaeScore.Classes
{
    class StatusToColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            SolidColorBrush retColor = new SolidColorBrush();
            retColor.Color = Color.FromRgb(0, 0, 0);
            if ((bool)value)
            {
                retColor.Color = Color.FromRgb(123, 200, 98);   //Green
            }
            else
            {
                retColor.Color = Color.FromRgb(255, 0, 0);      //Red
            }
            return retColor;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
