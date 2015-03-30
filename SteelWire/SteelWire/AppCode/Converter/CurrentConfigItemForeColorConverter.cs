using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace SteelWire.AppCode.Converter
{
    /// <summary>
    /// 配置项前景色转换器
    /// </summary>
    public class CurrentConfigItemForeColorConverter : IValueConverter
    {
        public object Convert(object value, Type typeTarget, object param, CultureInfo culture)
        {
            if (value is int)
            {
                if ((int)value <= 0)
                {
                    return new SolidColorBrush(Color.FromRgb(255, 0, 0));
                }
            }
            else if (value is decimal)
            {
                if ((decimal)value <= 0)
                {
                    return new SolidColorBrush(Color.FromRgb(255, 0, 0));
                }
            }
            return new SolidColorBrush(Color.FromRgb(0, 0, 0));
        }

        public object ConvertBack(object value, Type typeTarget, object param, CultureInfo culture)
        {
            return new object();
        }
    }
}