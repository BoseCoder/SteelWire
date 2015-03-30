using System;
using System.Globalization;
using System.Windows.Data;

namespace SteelWire.AppCode.Converter
{
    /// <summary>
    /// �������ı�ת����
    /// </summary>
    public class CurrentConfigItemTextConverter : IValueConverter
    {
        public object Convert(object value, Type typeTarget, object param, CultureInfo culture)
        {
            string format = param as string;
            if (!string.IsNullOrWhiteSpace(format))
            {
                return string.Format(format, value);
            }
            return string.Format("{0}", value);
        }

        public object ConvertBack(object value, Type typeTarget, object param, CultureInfo culture)
        {
            if (typeTarget == typeof(int))
            {
                int intResult;
                int.TryParse(string.Format("{0}", value), out intResult);
                return intResult;
            }
            if (typeTarget == typeof(decimal))
            {
                decimal decimalResult;
                decimal.TryParse(string.Format("{0}", value), out decimalResult);
                return decimalResult;
            }
            return value;
        }
    }
}