using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace SteelWire.AppCode.Converter
{
    /// <summary>
    /// 配置项前景色转换器
    /// </summary>
    public class CumulationItemForeColorConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values != null && values.Length == 2 && values[0] is decimal && values[1] is decimal)
            {
                decimal critical = (decimal)values[0];
                if (critical > decimal.Zero)
                {
                    decimal cumulation = (decimal)values[1];
                    if (cumulation > critical)
                    {
                        return new SolidColorBrush(Color.FromRgb(0xFF, 0, 0));
                    }
                    decimal line = critical * 0.6M;
                    byte r, g;
                    decimal outValue;
                    if (cumulation > line)
                    {
                        r = 0xFF;
                        outValue = (cumulation - line) / (critical - line);
                        g = (byte)System.Convert.ToInt32(0xA5 * (1 - outValue));
                    }
                    else
                    {
                        outValue = cumulation / line;
                        r = (byte)System.Convert.ToInt32(outValue * 0xFF);
                        g = 0xA5;
                    }
                    return new SolidColorBrush(Color.FromRgb(r, g, 0));
                }
            }
            return new SolidColorBrush(Color.FromRgb(0, 0, 255));
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}