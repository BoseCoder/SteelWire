using System;
using SteelWire.Business.Config;
using SteelWire.Language;

namespace SteelWire.AppCode.Dependencies
{
    public class DependencyDrillConfig
    {
        private DrillDeviceTypeEnum DrillDeviceType { get; }
        public DependencyObject<string> Name { get; }
        public DependencyLanguage LocalName { get; }
        public DependencyLanguage WeightTitle { get; }
        public DependencyObject<decimal> Weight { get; }
        public DependencyLanguage LengthTitle { get; }
        public DependencyObject<decimal> Length { get; }

        public DependencyDrillConfig(DrillDeviceTypeEnum drillDeviceType, string name, decimal weight, decimal length)
        {
            this.DrillDeviceType = drillDeviceType;
            this.Name = new DependencyObject<string>(name);
            this.LocalName = DependencyLanguage.Generate(() => LanguageManager.GetLocalResourceStringRight(
                $"{nameof(DrillDeviceTypeEnum)}{this.DrillDeviceType}", this.Name.Value));
            this.WeightTitle = DependencyLanguage.Generate(() => LanguageManager.GetLocalResourceStringLeft(
                $"{nameof(DrillDeviceTypeEnum)}{this.DrillDeviceType}{nameof(Weight)}"));
            this.Weight = new DependencyObject<decimal>(weight);
            this.LengthTitle = DependencyLanguage.Generate(() => LanguageManager.GetLocalResourceStringLeft(
                $"{nameof(DrillDeviceTypeEnum)}{this.DrillDeviceType}{nameof(Length)}"));
            this.Length = new DependencyObject<decimal>(length);
            this.Name.ValueChangedHandler += NameChanged;
        }

        private void NameChanged(object sender, EventArgs e)
        {
            this.LocalName.Value = this.LocalName.VlueGenerater?.Invoke();
        }
    }
}