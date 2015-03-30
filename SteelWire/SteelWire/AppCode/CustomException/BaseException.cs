using System;
using System.Windows;
using SteelWire.Lang;

namespace SteelWire.AppCode.CustomException
{
    public abstract class BaseException : Exception
    {
        private const string ErrorCodeKey = "Error";
        private readonly bool _showBox;
        private readonly string _code;

        public override string Message
        {
            get { return LanguageManager.GetLocalResourceStringRight(ErrorCodeKey, this._code); }
        }

        protected BaseException(string errorCode, bool showBox)
        {
            this._code = errorCode;
            this._showBox = showBox;
        }

        public virtual void Handle()
        {
            if (this._showBox)
            {
                ShowMessageBox();
            }
        }

        protected abstract void ShowMessageBox();

        public static void HandleUnknowException(Exception ex)
        {
            MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public static void HandleException(Exception ex)
        {
            BaseException knowException = ex as BaseException;
            if (knowException != null)
            {
                knowException.Handle();
            }
            else
            {
                HandleUnknowException(ex);
            }
        }
    }
}
