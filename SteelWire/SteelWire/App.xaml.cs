using System;
using System.Globalization;
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

            LoadLanguage();
        }

        private void LoadLanguage()
        {
            ResourceDictionary langResource;
            try
            {
                CultureInfo currentCultureInfo = CultureInfo.CurrentCulture;
                string langUrl = string.Format(@"Lang\{0}.xaml", currentCultureInfo.Name);
                langResource = LoadComponent(new Uri(langUrl, UriKind.Relative)) as ResourceDictionary;
            }
            catch (Exception)
            {
                langResource = null;
            }
            if (langResource != null)
            {
                if (this.Resources.MergedDictionaries.Count>0)
                {
                    this.Resources.MergedDictionaries.Clear();
                }
                this.Resources.MergedDictionaries.Add(langResource);
            }
        }
    }
}
