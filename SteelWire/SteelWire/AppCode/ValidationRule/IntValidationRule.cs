using System.Globalization;
using System.Windows.Controls;

namespace SteelWire.AppCode.ValidationRule
{
    public class IntValidationRule : System.Windows.Controls.ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string input = string.Format("{0}", value);
            if (string.IsNullOrWhiteSpace(input))
            {
                return new ValidationResult(false, "输入不能为空，请先输入整数！");
            }
            int result;
            if (!int.TryParse(input, out result))
            {
                return new ValidationResult(false, "请输入整数！");
            }
            return ValidationResult.ValidResult;
        }
    }
}