using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace SteelWire.AppCode.Converter
{
    public class StringConcatConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return string.Join(" ", values.Select(obj => obj is decimal ? string.Format("{0:F3}", obj) : obj));
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
