using System;
using System.Globalization;
using System.Windows.Data;

namespace SteelWire.AppCode.Converter
{
    /// <summary>
    /// 配置项文本转换器
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
            if (value == null)
            {
                return null;
            }
            Type regionType = value.GetType();
            if (regionType == typeof(string))
            {
                return value;
            }
            string text = $"{value}";
            if (regionType == typeof(decimal))
            {
                if (!text.Contains("."))
                {
                    text = text + ".";
                }
                return text;
            }
            return text;
        }

        public object ConvertBack(object value, Type typeTarget, object param, CultureInfo culture)
        {
            if (typeTarget == typeof(int))
            {
                int intResult;
                int.TryParse($"{value}", out intResult);
                return intResult;
            }
            if (typeTarget == typeof(decimal))
            {
                string tmp = $"{value}";
                if (tmp.StartsWith("."))
                {
                    tmp = "0" + tmp;
                }
                decimal decimalResult;
                decimal.TryParse(tmp, out decimalResult);
                return decimalResult;
            }
            return value;
        }
    }
}