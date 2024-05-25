using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Build_IT_Infrastructure.Resources.Converters
{
    public  class BoolToVisibilityReverseConverter : IValueConverter
    {
        #region Public_Methods
        
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion // Public_Methods
    }
}
