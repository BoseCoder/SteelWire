using System;
using System.Windows;

namespace SteelWire.Lang
{
    public static class LanguageManager
    {
        public static string GetLocalResourceStringLeft(string key, string name = null)
        {
            if(string.IsNullOrEmpty(key))
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
    }
}
