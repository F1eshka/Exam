using System;
using System.Globalization;
using System.Windows.Data;

namespace EkzamenADO.Converters
{
    public class PercentageWidthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double actualWidth && double.TryParse(parameter.ToString(), out double percent))
            {
                // Отнимаем примерно 35 пикселей на скролл и отступы
                return (actualWidth - 35) * percent;
            }

            return 100.0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
