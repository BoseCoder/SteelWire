using System.Windows;
using SteelWire.Lang;

namespace SteelWire.AppCode.CustomException
{
    /// <summary>
    /// 消息异常
    /// </summary>
    public class InfoException : BaseException
    {
        public InfoException(string errorCode)
            : base(errorCode, true)
        { }

        public InfoException(string errorCode, bool showBox)
            : base(errorCode, showBox)
        { }

        protected override void ShowMessageBox()
        {
            MessageBox.Show(this.Message, LanguageManager.GetLocalResourceStringRight(ErrorCodeKey, ErrorCaptionKey),
                MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
