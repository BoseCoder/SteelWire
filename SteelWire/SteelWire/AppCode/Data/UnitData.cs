using System;
using SteelWire.AppCode.Config;
using SteelWire.AppCode.Dependencies;
using SteelWire.Language;

namespace SteelWire.AppCode.Data
{
    public static class UnitData
    {
        public static DependencyLanguage Millimetre { get; } =
            DependencyLanguage.Generate(() => GenerateValue(UnitEnum.Millimetre));

        public static DependencyLanguage Metre { get; } =
            DependencyLanguage.Generate(() => GenerateValue(UnitEnum.Metre));

        public static DependencyLanguage Kilogram { get; } =
            DependencyLanguage.Generate(() => GenerateValue(UnitEnum.Kilogram));

        public static DependencyLanguage KilogramPerMetre { get; } =
            DependencyLanguage.Generate(() => GenerateValue(UnitEnum.KilogramPerMetre));

        public static DependencyLanguage GramPerCubicCentimetre { get; } =
            DependencyLanguage.Generate(() => GenerateValue(UnitEnum.GramPerCubicCentimetre));

        public static DependencyLanguage KilogramPerCubicMeter { get; } =
            DependencyLanguage.Generate(() => GenerateValue(UnitEnum.KilogramPerCubicMeter));

        public static DependencyLanguage Kilonewton { get; } =
            DependencyLanguage.Generate(() => GenerateValue(UnitEnum.Kilonewton));

        public static DependencyLanguage Ton { get; } =
            DependencyLanguage.Generate(() => GenerateValue(UnitEnum.Ton));

        public static DependencyLanguage TonKilometre { get; } =
            DependencyLanguage.Generate(() => GenerateValue(UnitEnum.TonKilometre));

        static UnitData()
        {
            GlobalData.Wireline.UnitSystem.Value = SystemConfigManager.OnceInstance.UnitSystem;
            GlobalData.Wireline.UnitSystem.ValueChangedHandler += UnitSystemChanged;
        }

        private static void UnitSystemChanged(object sender, EventArgs e)
        {
            Millimetre.Value = Millimetre.VlueGenerater?.Invoke();
            Metre.Value = Metre.VlueGenerater?.Invoke();
            Kilogram.Value = Kilogram.VlueGenerater?.Invoke();
            KilogramPerMetre.Value = KilogramPerMetre.VlueGenerater?.Invoke();
            GramPerCubicCentimetre.Value = GramPerCubicCentimetre.VlueGenerater?.Invoke();
            KilogramPerCubicMeter.Value = KilogramPerCubicMeter.VlueGenerater?.Invoke();
            Kilonewton.Value = Kilonewton.VlueGenerater?.Invoke();
            Ton.Value = Ton.VlueGenerater?.Invoke();
            TonKilometre.Value = TonKilometre.VlueGenerater?.Invoke();
        }

        private static string GenerateValue(UnitEnum unit)
        {
            return LanguageManager.GetLocalResourceStringLeft($"{GlobalData.Wireline.UnitSystem.Value}{unit}");
        }
    }
}
