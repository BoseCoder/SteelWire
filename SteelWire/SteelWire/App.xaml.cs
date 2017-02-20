using System;
using System.Windows;
using SteelWire.AppCode.Config;
using SteelWire.AppCode.Data;
using SteelWire.Language;

namespace SteelWire
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            GlobalData.Language.Value = SystemConfigManager.OnceInstance.Language;
            LanguageManager.LoadLanguage(GlobalData.Language.Value);
            GlobalData.Language.ValueChangedHandler += LanguageChanged;
        }

        private static void LanguageChanged(object sender, EventArgs e)
        {
            LanguageManager.LoadLanguage(GlobalData.Language.Value);
        }
    }
}
