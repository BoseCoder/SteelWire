using System.ComponentModel;
using System.Windows.Input;
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

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            Keyboard.Focus(this.TxtAccount);
        }

        private void OpenOption(object sender, MouseButtonEventArgs e)
        {
            OptionWindow option = new OptionWindow();
            option.ShowDialog();
        }

        private void AccountPreviewKeyDownEvent(object sender, KeyEventArgs e)
        {
            if ((e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
                || (e.Key >= Key.D0 && e.Key <= Key.D9)
                || (e.Key >= Key.A && e.Key <= Key.Z)
                || e.Key == Key.Back || e.Key == Key.Delete
                || e.Key == Key.Home || e.Key == Key.End
                || e.Key == Key.Left || e.Key == Key.Right
                || e.Key == Key.Tab || e.Key == Key.Enter)
            {
                if (e.KeyboardDevice.Modifiers != ModifierKeys.None)
                {
                    e.Handled = true;
                }
            }
            else
            {
                e.Handled = true;
            }
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
                Sign.Data.Regist(this.TxtAccount.Text.Trim(), this.PassBox.Password.Trim(), this.PassComfirmBox.Password.Trim(), this.TxtName.Text.Trim());
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
            Sign.Data.IsRegist.ItemValue = false;
            if (!this.DialogResult.HasValue)
            {
                this.DialogResult = Sign.Data.IsSignIn();
            }
        }

        private void WindowOnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void AccountKeyDown(object sender, KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.Key == Key.Enter && !string.IsNullOrWhiteSpace(this.TxtAccount.Text))
            {
                this.PassBox.Focus();
            }
        }

        private void PassOnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && Sign.Data.IsSign.ItemValue
                && !string.IsNullOrWhiteSpace(this.TxtAccount.Text)
                && !string.IsNullOrWhiteSpace(this.PassBox.Password))
            {
                SignIn(sender, e);
            }
        }
    }
}
