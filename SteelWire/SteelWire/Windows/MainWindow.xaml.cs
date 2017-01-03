using System.ComponentModel;
using SteelWire.AppCode.Config;
using SteelWire.AppCode.CustomException;
using SteelWire.AppCode.CustomMessage;
using SteelWire.WindowData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SteelWire.AppCode.Data;
using SteelWire.AppCode.Dependencies;
using SteelWire.Language;


namespace SteelWire.Windows
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow
    {
        public Main WindowData { get; }

        public MainWindow()
        {
            InitializeComponent();

            this.WindowData = this.DataContext as Main;

            if (this.WindowData == null)
            {
                this.Close();
                return;
            }

            ShowOptionWindow();

            if (!this.WindowData.ShowSignWindow())
            {
                this.Close();
                return;
            }

            ShowOptionWindow();

            InitializeComboBox();

            this.WindowData.WireropeWorkloadCollection.ItemsChangedHandler += WireropeWorkloadCollectionChanged;
        }

        private void WireropeWorkloadCollectionChanged(object sender, EventArgs e)
        {
            this.CboWirelineDiameter.ItemsSource = this.WindowData.WireropeWorkloadCollection.Items.Select(w => w.Value.Name);
        }

        private void InitializeComboBox()
        {
            CuttingCriticalDictionary cuttingCriticalDic = CuttingCriticalDictionaryManager.OnceInstance.DictionarySection;
            this.CboRopeCount.ItemsSource = cuttingCriticalDic.WireropeEfficiencies.Cast<WireropeEfficiency>().Select(e => e.Count);

            this.CboWirelineDiameter.ItemsSource = this.WindowData.WireropeWorkloadCollection.Items.Select(w => w.Value.Name);

            Type drillingTypeEnumType = typeof(DrillingTypeEnum);
            this.CboDrillingType.ItemsSource =
                Enum.GetValues(drillingTypeEnumType).Cast<DrillingTypeEnum>().Select(value =>
                    new KeyValuePair<DrillingTypeEnum, DependencyLanguage>(value, DependencyLanguage.Generate(() =>
                        LanguageManager.GetLocalResourceStringLeft(drillingTypeEnumType.Name, value.ToString()))));
        }

        private void ShowOptionWindow()
        {
            if (!SystemConfigManager.OnceInstance.Run)
            {
                OptionWindow option = new OptionWindow();
                option.ShowDialog();
                SystemConfigManager.OnceInstance.Run = true;
                SystemConfigManager.OnceInstance.SaveConfig();
            }
        }

        #region Action

        private void WindowRendered(object sender, EventArgs e)
        {
            try
            {
                if (this.WindowData.CheckNeedReset())
                {
                    this.WindowData.Reset(true);
                }
                this.WindowData.CanCancelExit = true;
            }
            catch (Exception ex)
            {
                BaseException.HandleException(ex);
            }
        }

        private void WindowOnClosing(object sender, CancelEventArgs e)
        {
            try
            {
                if (this.WindowData.CanCancelExit && !MessageManager.Question("ExitConfirm"))
                {
                    e.Cancel = true;
                }
                else if (ReportCutWindow.CurrentWindow != null)
                {
                    ReportCutWindow.CurrentWindow.Close();
                }
            }
            catch (Exception ex)
            {
                BaseException.HandleException(ex);
            }
        }

        private void Refresh(object sender, RoutedEventArgs e)
        {
            try
            {
                TimeMeter.OnceInstance.CurrentTime = DateTime.Now;
                this.WindowData.RefreshData();
                if (this.WindowData.CheckNeedReset())
                {
                    this.WindowData.Reset(true);
                }
            }
            catch (Exception ex)
            {
                BaseException.HandleException(ex);
            }
        }

        private void Cumulate(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.WindowData.Cumulate())
                {
                    if (this.WindowData.CheckNeedReset())
                    {
                        this.WindowData.Reset(true);
                    }
                }
            }
            catch (Exception ex)
            {
                BaseException.HandleException(ex);
            }
        }

        private void Reset(object sender, RoutedEventArgs e)
        {
            try
            {
                this.WindowData.Reset(false);
            }
            catch (Exception ex)
            {
                BaseException.HandleException(ex);
            }
        }

        private void SwitchWire(object sender, RoutedEventArgs e)
        {
            try
            {
                this.WindowData.WirelineNumber.Value = string.Empty;
                this.WindowData.RefreshData();
            }
            catch (Exception ex)
            {
                BaseException.HandleException(ex);
            }
        }

        #endregion

        #region Input Limit

        private void DecimalBoxPreviewKeyDownEvent(object sender, KeyEventArgs e)
        {
            NumericBoxPreviewKeyDownEvent(sender, e, true);
        }

        private void IntBoxPreviewKeyDownEvent(object sender, KeyEventArgs e)
        {
            NumericBoxPreviewKeyDownEvent(sender, e, false);
        }

        private static void NumericBoxPreviewKeyDownEvent(object sender, KeyEventArgs e, bool allowDecimal)
        {
            if (e.KeyboardDevice.Modifiers == ModifierKeys.Control)
            {
                return;
            }
            if ((e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
                || (e.Key >= Key.D0 && e.Key <= Key.D9)
                || e.Key == Key.Back || e.Key == Key.Delete
                || e.Key == Key.Home || e.Key == Key.End
                || e.Key == Key.Left || e.Key == Key.Right
                || e.Key == Key.Tab)
            {
                if (e.KeyboardDevice.Modifiers != ModifierKeys.None)
                {
                    e.Handled = true;
                }
                return;
            }
            if (allowDecimal && (e.Key == Key.Decimal))
            {
                TextBox txtBox = (TextBox)sender;
                if (txtBox.Text.Contains("."))
                {
                    e.Handled = true;
                }
                return;
            }
            e.Handled = true;
        }

        #endregion

        private void SignOut(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.WindowData.ShowSignWindow())
                {
                    ShowOptionWindow();
                    if (this.WindowData.CheckNeedReset())
                    {
                        this.WindowData.Reset(true);
                    }
                }
            }
            catch (Exception ex)
            {
                BaseException.HandleException(ex);
            }
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                BaseException.HandleException(ex);
            }
        }

        private void OpenOption(object sender, RoutedEventArgs e)
        {
            try
            {
                OptionWindow option = new OptionWindow();
                option.ShowDialog();
            }
            catch (Exception ex)
            {
                BaseException.HandleException(ex);
            }
        }

        private void OpenReportCut(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ReportCutWindow.CurrentWindow == null)
                {
                    ReportCutWindow reportCutWindow = new ReportCutWindow();
                    reportCutWindow.Show();
                }
                else if (!ReportCutWindow.CurrentWindow.IsActive)
                {
                    ReportCutWindow.CurrentWindow.Activate();
                }
            }
            catch (Exception ex)
            {
                BaseException.HandleException(ex);
            }
        }
    }
}
