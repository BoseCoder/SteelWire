using SteelWire.AppCode;
using SteelWire.AppCode.Config;
using SteelWire.AppCode.CustomException;
using SteelWire.AppCode.Dependencies;
using SteelWire.Lang;
using SteelWire.WindowData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace SteelWire.Windows
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            SignWindow signWindow = new SignWindow();
            if (signWindow.ShowDialog() == true)
            {
                CuttingCriticalDictionaryManager.InitializeConfig();
                CuttingCriticalConfigManager.InitializeConfig();
                WorkDictionaryManager.InitializeConfig();
                WorkConfigManager.InitializeConfig();
                Main.Data.InitializeData();
                Main.Data.RefreshData();
            }
            else
            {
                this.Close();
                return;
            }

            CuttingCriticalDictionary cuttingCriticalDic = CuttingCriticalDictionaryManager.OnceInstance.DictionarySection;
            this.CboWirelineDiameter.ItemsSource = cuttingCriticalDic.WireropeWorkloads.Cast<WireropeWorkload>().Select(l => l.Diameter);
            this.CboRopeCount.ItemsSource = cuttingCriticalDic.WireropeEfficiencies.Cast<WireropeEfficiency>().Select(e => e.Count);

            WorkDictionary workDic = WorkDictionaryManager.OnceInstance.DictionarySection;
            this.CboDrillingType.ItemsSource = workDic.DrillingTypes.Cast<DrillingType>()
                .Select(t => new KeyValuePair<string, string>(t.Name, LanguageManager.GetLocalResourceStringLeft("DrillingType", t.Name)));
            this.CboDrillingDifficulty.ItemsSource = workDic.DrillingDifficulties.Cast<DrillingDifficulty>()
                .Select(d => new KeyValuePair<string, string>(d.Name, LanguageManager.GetLocalResourceStringLeft("DrillingDifficulty", d.Name)));

            DrillPipeConfig.StaticWeightTitle.ItemValue = LanguageManager.GetLocalResourceStringLeft("DrillPipeWeight");
            DrillPipeConfig.StaticLengthTitle.ItemValue = LanguageManager.GetLocalResourceStringLeft("DrillPipeLength");
            DrillCollarConfig.StaticWeightTitle.ItemValue = LanguageManager.GetLocalResourceStringLeft("DrillCollarWeight");
            DrillCollarConfig.StaticLengthTitle.ItemValue = LanguageManager.GetLocalResourceStringLeft("DrillCollarLength");
        }

        #region Action

        private void Refresh(object sender, RoutedEventArgs e)
        {
            try
            {
                TimeMeter.OnceInstance.CurrentTime = DateTime.Now;
                Main.Data.RefreshData();
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
                Main.Data.Cumulate();
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
                Main.Data.Reset();
                Main.Data.RefreshData();
            }
            catch (Exception ex)
            {
                BaseException.HandleException(ex);
            }
        }

        #endregion

        #region Input Limit

        private void IntBoxPreviewKeyDownEvent(object sender, KeyEventArgs e)
        {
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
            }
            else
            {
                e.Handled = true;
            }
        }

        private void DecimalBoxPreviewKeyDownEvent(object sender, KeyEventArgs e)
        {
            if ((e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
                || (e.Key >= Key.D0 && e.Key <= Key.D9)
                || e.Key == Key.Decimal
                || e.Key == Key.Back || e.Key == Key.Delete
                || e.Key == Key.Home || e.Key == Key.End
                || e.Key == Key.Left || e.Key == Key.Right
                || e.Key == Key.Tab)
            {
                if (e.KeyboardDevice.Modifiers != ModifierKeys.None)
                {
                    e.Handled = true;
                    return;
                }
                TextBox txtBox = (TextBox)sender;
                if (e.Key == Key.Decimal)
                {
                    if (txtBox.Text.Contains(".") || txtBox.SelectionStart < 1)
                    {
                        e.Handled = true;
                    }
                }
            }
            else
            {
                e.Handled = true;
            }
        }

        #endregion

        private void SignOut(object sender, RoutedEventArgs e)
        {
            SignWindow signWindow = new SignWindow();
            if (signWindow.ShowDialog() == true)
            {
                CuttingCriticalDictionaryManager.InitializeConfig();
                CuttingCriticalConfigManager.InitializeConfig();
                WorkDictionaryManager.InitializeConfig();
                WorkConfigManager.InitializeConfig();
                Main.Data.InitializeData();
                Main.Data.RefreshData();
            }
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void LanguageChange(object sender, RoutedEventArgs e)
        {
            if (Equals(sender, this.MenuItemEnglish))
            {
                LanguageManager.LoadLanguage("en-US");
            }
            else if (Equals(sender, this.MenuItemChinese))
            {
                LanguageManager.LoadLanguage("zh-CN");
            }
        }
    }
}
