using System;
using System.Globalization;
using System.Windows.Data;

namespace SteelWire.AppCode.Converter
{
    public class StringConcatConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return string.Join(" ", values);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
