using System;
using System.Windows;
using SteelWire.Language;

namespace SteelWire.AppCode.CustomException
{
    /// <summary>
    /// 消息异常
    /// </summary>
    public class InfoException : BaseException
    {
        /// <summary>
        /// 多语言错误内容前缀Key
        /// </summary>
        public const string MessageCodeKey = "Message";

        public InfoException(string errorCode)
            : base(errorCode, true, null)
        { }

        public InfoException(string errorCode, Exception ex)
            : base(errorCode, true, ex)
        { }

        public InfoException(string errorCode, bool showBox, Exception ex)
            : base(errorCode, showBox, ex)
        { }

        protected override void ShowMessageBox()
        {
            MessageBox.Show(this.Message, LanguageManager.GetLocalResourceStringRight(MessageCodeKey, ErrorCaptionKey),
                MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
