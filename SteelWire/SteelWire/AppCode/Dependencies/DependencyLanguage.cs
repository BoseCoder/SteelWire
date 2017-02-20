using System;
using System.Collections.Generic;
using SteelWire.AppCode.Data;

namespace SteelWire.AppCode.Dependencies
{
    public class DependencyLanguage : DependencyObject<string>
    {
        public static List<DependencyLanguage> DependencyLanguageObjects { get; } = new List<DependencyLanguage>();

        static DependencyLanguage()
        {
            GlobalData.Language.ValueChangedHandler += LanguageChanged;
        }

        private static string GenerateValue(Func<string> valueGenerater)
        {
            return valueGenerater == null ? string.Empty : valueGenerater.Invoke();
        }

        public static DependencyLanguage Generate(Func<string> valueGenerater)
        {
            DependencyLanguage languageObject = new DependencyLanguage(valueGenerater);
            DependencyLanguageObjects.Add(languageObject);
            return languageObject;
        }

        private static void LanguageChanged(object sender, EventArgs e)
        {
            foreach (DependencyLanguage languageObject in DependencyLanguageObjects)
            {
                languageObject.Value = GenerateValue(languageObject.VlueGenerater);
            }
        }

        private DependencyLanguage(Func<string> valueGenerater)
            : base(GenerateValue(valueGenerater))
        {
            this.VlueGenerater = valueGenerater;
        }

        public Func<string> VlueGenerater { get; set; }
    }
}
