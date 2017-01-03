using System;
using System.Globalization;
using System.Windows.Data;

namespace SteelWire.AppCode.Converter
{
    /// <summary>
    /// 计时器文本转换器
    /// </summary>
    public class TimeMeterTextConverter : IValueConverter
    {
        public object Convert(object value, Type typeTarget, object param, CultureInfo culture)
        {
            string format = param as string;
            if (!string.IsNullOrWhiteSpace(format))
            {
                return string.Format(format, value);
            }
            if (!(value is DateTime))
            {
                return $"{value}";
            }
            DateTime now = (DateTime)value;
            string[] formates = now.GetDateTimeFormats('D');
            if (formates.Length > 1)
            {
                return formates[1];
            }
            return now.ToShortDateString();
        }

        public object ConvertBack(object value, Type typeTarget, object param, CultureInfo culture)
        {
            return DateTime.Now;
        }
    }
}