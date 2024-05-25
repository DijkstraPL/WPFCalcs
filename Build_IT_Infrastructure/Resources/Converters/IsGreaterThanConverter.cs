using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace Build_IT_Infrastructure.Resources.Converters
{
    public class IsGreaterThanConverter : IMultiValueConverter, IValueConverter
    {
        #region Public_Methods
        
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((IComparable)value).CompareTo((IComparable)parameter) > 0;
        }

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return ((IComparable)values[0]).CompareTo((IComparable)values[1]) > 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion // Public_Methods
    }
}
