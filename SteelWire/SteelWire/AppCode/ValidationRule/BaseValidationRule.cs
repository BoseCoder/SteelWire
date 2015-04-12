namespace SteelWire.AppCode.ValidationRule
{
    /// <summary>
    /// 输入验证规则基类
    /// </summary>
    public abstract class BaseValidationRule : System.Windows.Controls.ValidationRule
    {
        /// <summary>
        /// 规则出错的多语言消息前缀Key
        /// </summary>
        public const string ValidationCodeKey = "Validation";
    }
}
