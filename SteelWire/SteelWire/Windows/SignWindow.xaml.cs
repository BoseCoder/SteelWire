using System.ComponentModel;
using SteelWire.AppCode.CustomException;
using SteelWire.WindowData;
using System;
using System.Windows;

namespace SteelWire.Windows
{
    /// <summary>
    /// SignWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SignWindow
    {
        public SignWindow()
        {
            InitializeComponent();
        }

        private void ModeChange(object sender, RoutedEventArgs e)
        {
            Sign.Data.IsRegist.ItemValue = true;
        }

        private void SignIn(object sender, RoutedEventArgs e)
        {
            try
            {
                Sign.Data.SignIn(this.TxtAccount.Text.Trim(), this.PassBox.Password.Trim());
                this.Close();
            }
            catch (Exception ex)
            {
                BaseException.HandleException(ex);
            }
        }

        private void Regist(object sender, RoutedEventArgs e)
        {
            try
            {
                Sign.Data.Regist(this.TxtAccount.Text.Trim(), this.PassBox.Password.Trim(), this.TxtName.Text.Trim());
                this.Close();
            }
            catch (Exception ex)
            {
                BaseException.HandleException(ex);
            }
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void WindowClosing(object sender, CancelEventArgs e)
        {
            if (!this.DialogResult.HasValue)
            {
                this.DialogResult = Sign.Data.IsSignIn();
            }
        }
    }
}
