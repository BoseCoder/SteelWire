using System;
using System.Windows;
using SteelWire.Lang;

namespace SteelWire.AppCode.CustomException
{
    /// <summary>
    /// 异常基类
    /// </summary>
    public abstract class BaseException : Exception
    {
        /// <summary>
        /// 多语言错误提示标题Key
        /// </summary>
        public const string ErrorCaptionKey = "Caption";
        /// <summary>
        /// 多语言错误内容前缀Key
        /// </summary>
        public const string ErrorCodeKey = "Error";
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

        /// <summary>
        /// 处理方法
        /// </summary>
        public virtual void Handle()
        {
            if (this._showBox)
            {
                ShowMessageBox();
            }
        }

        protected abstract void ShowMessageBox();

        /// <summary>
        /// 处理未知异常
        /// </summary>
        /// <param name="ex">未知异常</param>
        public static void HandleUnknowException(Exception ex)
        {
            MessageBox.Show(ex.Message, LanguageManager.GetLocalResourceStringRight(ErrorCodeKey, ErrorCaptionKey),
                MessageBoxButton.OK, MessageBoxImage.Error);
        }

        /// <summary>
        /// 处理异常
        /// </summary>
        /// <param name="ex">异常</param>
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
