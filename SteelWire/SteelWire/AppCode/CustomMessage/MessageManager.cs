using SteelWire.Lang;
using System.Windows;

namespace SteelWire.AppCode.CustomMessage
{
    /// <summary>
    /// 对语言消息管理
    /// </summary>
    public class MessageManager
    {
        /// <summary>
        /// 多语言消息提示标题Key
        /// </summary>
        private const string MessageCaptionKey = "Caption";
        /// <summary>
        /// 多语言消息内容前缀Key
        /// </summary>
        private const string MessageCodeKey = "Message";

        private static bool? CaseChoose(MessageBoxResult result)
        {
            switch (result)
            {
                case MessageBoxResult.Yes:
                    return true;
                case MessageBoxResult.No:
                    return false;
                default:
                    return null;
            }
        }

        public static bool? Choose(string messageCode)
        {
            MessageBoxResult result = MessageBox.Show(LanguageManager.GetLocalResourceStringRight(MessageCodeKey, messageCode),
                LanguageManager.GetLocalResourceStringRight(MessageCodeKey, MessageCaptionKey),
                MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            return CaseChoose(result);
        }

        public static bool Question(string messageCode)
        {
            return MessageBox.Show(LanguageManager.GetLocalResourceStringRight(MessageCodeKey, messageCode),
                LanguageManager.GetLocalResourceStringRight(MessageCodeKey, MessageCaptionKey),
                MessageBoxButton.YesNo, MessageBoxImage.Exclamation) == MessageBoxResult.Yes;
        }

        public static bool Warning(string messageCode)
        {
            return MessageBox.Show(LanguageManager.GetLocalResourceStringRight(MessageCodeKey, messageCode),
                LanguageManager.GetLocalResourceStringRight(MessageCodeKey, MessageCaptionKey),
                MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK;
        }

        public static bool? WarningChoose(string messageCode)
        {
            MessageBoxResult result = MessageBox.Show(LanguageManager.GetLocalResourceStringRight(MessageCodeKey, messageCode),
                LanguageManager.GetLocalResourceStringRight(MessageCodeKey, MessageCaptionKey),
                MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
            return CaseChoose(result);
        }
    }
}
