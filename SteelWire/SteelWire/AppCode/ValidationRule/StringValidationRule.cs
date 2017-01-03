using System.Globalization;
using System.Windows.Controls;
using SteelWire.Language;

namespace SteelWire.AppCode.ValidationRule
{
    /// <summary>
    /// 整数输入验证
    /// </summary>
    public class StringValidationRule : BaseValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string input = $"{value}";
            if (string.IsNullOrWhiteSpace(input))
            {
                return new ValidationResult(false, LanguageManager.GetLocalResourceStringLeft(ValidationCodeKey, "StringEmpty"));
            }
            return ValidationResult.ValidResult;
        }
    }
}