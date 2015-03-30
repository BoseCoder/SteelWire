using System.Globalization;
using System.Windows.Controls;

namespace SteelWire.AppCode.ValidationRule
{
    public class DecimalValidationRule : System.Windows.Controls.ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string input = string.Format("{0}", value);
            if (string.IsNullOrWhiteSpace(input))
            {
                return new ValidationResult(false, "输入不能为空，请先输入数字！");
            }
            decimal result;
            if (!decimal.TryParse(input, out result))
            {
                return new ValidationResult(false, "请输入数字！");
            }
            return ValidationResult.ValidResult;
        }
    }
}