using System.Globalization;
using System.Windows.Controls;
using SteelWire.Language;

namespace SteelWire.AppCode.ValidationRule
{
    /// <summary>
    /// 小数输入验证
    /// </summary>
    public class DecimalValidationRule : BaseValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string input = $"{value}";
            if (string.IsNullOrWhiteSpace(input))
            {
                return new ValidationResult(false, LanguageManager.GetLocalResourceStringRight(ValidationCodeKey, "DecimalEmpty"));
            }
            decimal result;
            if (!decimal.TryParse(input, out result))
            {
                return new ValidationResult(false, LanguageManager.GetLocalResourceStringRight(ValidationCodeKey, "DecimalError"));
            }
            return ValidationResult.ValidResult;
        }
    }
}