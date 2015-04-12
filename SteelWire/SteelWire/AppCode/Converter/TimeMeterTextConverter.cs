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
                return string.Format("{0}", value);
            }
            DateTime now = (DateTime)value;
            return now.GetDateTimeFormats('D')[1];
        }

        public object ConvertBack(object value, Type typeTarget, object param, CultureInfo culture)
        {
            return DateTime.Now;
        }
    }
}