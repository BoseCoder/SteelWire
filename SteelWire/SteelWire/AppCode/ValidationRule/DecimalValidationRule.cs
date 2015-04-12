using System.Globalization;
using System.Windows.Controls;
using SteelWire.Lang;

namespace SteelWire.AppCode.ValidationRule
{
    /// <summary>
    /// С��������֤
    /// </summary>
    public class DecimalValidationRule : BaseValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string input = string.Format("{0}", value);
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