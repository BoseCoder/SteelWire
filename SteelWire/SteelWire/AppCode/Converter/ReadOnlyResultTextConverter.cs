using System;
using System.Globalization;
using System.Windows.Data;

namespace SteelWire.AppCode.Converter
{
    public class ReadOnlyResultTextConverter : IValueConverter
    {
        public object Convert(object value, Type typeTarget, object param, CultureInfo culture)
        {
            if (value is int)
            {
                if ((int)value < 0)
                {
                    return "Error";
                }
            }
            if (value is decimal)
            {
                if ((decimal)value < 0)
                {
                    return "Error";
                }
            }
            string format = param as string;
            if (!string.IsNullOrWhiteSpace(format))
            {
                return string.Format(format, value);
            }
            if (value is decimal)
            {
                return string.Format("{0:F3}", value);
            }
            return string.Format("{0}", value);
        }

        public object ConvertBack(object value, Type typeTarget, object param, CultureInfo culture)
        {
            return new object();
        }
    }
}