﻿using System;
using System.Globalization;
using System.Threading;
using System.Windows;

namespace SteelWire.Lang
{
    public static class LanguageManager
    {
        public static string GetLocalResourceStringLeft(string key, string name = null)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key");
            }
            try
            {
                string res = Application.Current.FindResource(string.Format("{0}{1}", key, name)) as string;
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
                throw new ArgumentNullException("name");
            }
            try
            {
                string res = Application.Current.FindResource(string.Format("{0}{1}", key, name)) as string;
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

        public static void LoadLanguage(string langName = null)
        {
            ResourceDictionary langResource;
            try
            {
                CultureInfo info;
                if (!string.IsNullOrEmpty(langName))
                {
                    info = CultureInfo.GetCultureInfo("en-US");
                    Thread.CurrentThread.CurrentUICulture = info;
                    Thread.CurrentThread.CurrentCulture = info;
                }
                else
                {
                    info = Thread.CurrentThread.CurrentUICulture;
                }
                string langUrl = string.Format(@"Lang\{0}.xaml", info.Name);
                langResource = Application.LoadComponent(new Uri(langUrl, UriKind.Relative)) as ResourceDictionary;
            }
            catch (Exception)
            {
                langResource = null;
            }
            if (langResource != null)
            {
                if (Application.Current.Resources.MergedDictionaries.Count > 0)
                {
                    Application.Current.Resources.MergedDictionaries.Clear();
                }
                Application.Current.Resources.MergedDictionaries.Add(langResource);
            }
        }
    }
}