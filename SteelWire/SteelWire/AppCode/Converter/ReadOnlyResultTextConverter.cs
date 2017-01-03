using System;
using System.Globalization;
using System.Windows.Data;

namespace SteelWire.AppCode.Converter
{
    /// <summary>
    /// 只读文本转换器
    /// </summary>
    public class ReadOnlyResultTextConverter : IValueConverter
    {
        public object Convert(object value, Type typeTarget, object param, CultureInfo culture)
        {
            string format = param as string;
            if (value != null)
            {
                Type dataType = value.GetType();
                if (dataType == typeof(int))
                {
                    if ((int)value < 0)
                    {
                        return "Error";
                    }
                }
                else if (dataType == typeof(decimal))
                {
                    if ((decimal)value < 0)
                    {
                        return "Error";
                    }
                    if (string.IsNullOrWhiteSpace(format))
                    {
                        return $"{value:F3}";
                    }
                }
            }
            if (!string.IsNullOrWhiteSpace(format))
            {
                return string.Format(format, value);
            }
            return $"{value}";
        }

        public object ConvertBack(object value, Type typeTarget, object param, CultureInfo culture)
        {
            return new object();
        }
    }
}