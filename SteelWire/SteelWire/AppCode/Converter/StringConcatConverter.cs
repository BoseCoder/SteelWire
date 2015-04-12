using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace SteelWire.AppCode.Converter
{
    /// <summary>
    /// 连接文本转换器
    /// </summary>
    public class StringConcatConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return string.Join(" ", values.Select(obj => obj is decimal ? ((decimal)obj > 0 ? string.Format("{0:F3}", obj) : null) : obj));
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
