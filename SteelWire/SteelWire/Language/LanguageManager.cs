using System;
using System.Globalization;
using System.Threading;
using System.Windows;
using SteelWire.AppCode.Config;

namespace SteelWire.Language
{
    public static class LanguageManager
    {
        public static string GetLocalResourceStringLeft(string key, string name = null)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }
            try
            {
                string res = Application.Current.FindResource($"{key}{name}") as string;
                if (string.IsNullOrEmpty(res))
                {
                    return key;
                }
                return res;
            }
            catch (Exception)
            {
                return key;
            }
        }

        public static string GetLocalResourceStringRight(string key, string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }
            try
            {
                string res = Application.Current.FindResource($"{key}{name}") as string;
                if (string.IsNullOrEmpty(res))
                {
                    return name;
                }
                return res;
            }
            catch (Exception)
            {
                return name;
            }
        }

        public static void LoadLanguage(LanguageEnum language)
        {
            ResourceDictionary langResource;
            try
            {
                CultureInfo info = (ConstDictionary.LanguageDictionary.ContainsKey(language)
                    ? ConstDictionary.LanguageDictionary[language]
                    : Thread.CurrentThread.CurrentCulture) ??
                           Thread.CurrentThread.CurrentCulture;
                string langUrl = $@"Lang\{info.Name}.xaml";
                langResource = new ResourceDictionary
                {
                    Source = new Uri(langUrl, UriKind.RelativeOrAbsolute)
                };
            }
            catch (Exception)
            {
                langResource = null;
            }
            if (langResource != null && Application.Current.Resources.MergedDictionaries.Count > 0)
            {
                Application.Current.Resources.MergedDictionaries[0] = langResource;
            }
        }
    }
}
