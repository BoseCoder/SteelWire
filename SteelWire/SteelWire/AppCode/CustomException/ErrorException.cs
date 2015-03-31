using System.Windows;
using SteelWire.Lang;

namespace SteelWire.AppCode.CustomException
{
    public class ErrorException : BaseException
    {
        public ErrorException(string errorCode)
            : base(errorCode, true)
        { }

        public ErrorException(string errorCode,bool showBox)
            : base(errorCode, showBox)
        { }

        protected override void ShowMessageBox()
        {
            MessageBox.Show(this.Message, LanguageManager.GetLocalResourceStringRight(ErrorCodeKey, ErrorCaptionKey),
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
