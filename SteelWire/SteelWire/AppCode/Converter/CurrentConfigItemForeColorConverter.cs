using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace SteelWire.AppCode.Converter
{
    /// <summary>
    /// ������ǰ��ɫת����
    /// </summary>
    public class CurrentConfigItemForeColorConverter : IValueConverter
    {
        public object Convert(object value, Type typeTarget, object param, CultureInfo culture)
        {
            if (value == null)
            {
                return new SolidColorBrush(Color.FromRgb(0x99, 0x99, 0x99));
            }
            Type dataType = value.GetType();
            if (dataType == typeof(int))
            {
                if ((int)value <= 0)
                {
                    return new SolidColorBrush(Color.FromRgb(255, 0, 0));
                }
            }
            else if (dataType == typeof(decimal))
            {
                if ((decimal)value <= 0)
                {
                    return new SolidColorBrush(Color.FromRgb(255, 0, 0));
                }
            }
            return new SolidColorBrush(Color.FromRgb(0x99, 0x99, 0x99));
        }

        public object ConvertBack(object value, Type typeTarget, object param, CultureInfo culture)
        {
            return new object();
        }
    }
}