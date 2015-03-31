using SteelWire.Lang;
using System.Windows;

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

            LanguageManager.LoadLanguage();
        }
    }
}
