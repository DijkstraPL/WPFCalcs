using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace Build_IT_Infrastructure.Resources.Converters
{
    public class MultiplyByConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var stringValue = value.ToString().Replace(',', '.');
            var stringParameter = parameter.ToString().Replace(',', '.');

            if (Double.TryParse(stringValue, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out double doubleValue) && 
                Double.TryParse(stringParameter, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out double parameterParameter))
                return Math.Round(doubleValue * parameterParameter);
            return stringValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var stringValue = value.ToString().Replace(',', '.');
            var stringParameter = parameter.ToString().Replace(',', '.');

            if (Double.TryParse(stringValue, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out double doubleValue) &&
                Double.TryParse(stringParameter, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out double parameterParameter))
                return doubleValue / parameterParameter;
            return stringValue;
        }
    }
}
