using SteelWire.AppCode.Config;
using SteelWire.AppCode.Data;

namespace SteelWire.WindowData
{
    public class Option
    {
        public static void ReadConfig()
        {
            if (GlobalData.IsSignIn)
            {
                GlobalData.Language.Value = UserConfigManager.OnceInstance.Language;
                GlobalData.Wireline.UnitSystem.Value = UserConfigManager.OnceInstance.UnitSystem;
            }
            else
            {
                GlobalData.Language.Value = SystemConfigManager.OnceInstance.Language;
                GlobalData.Wireline.UnitSystem.Value = SystemConfigManager.OnceInstance.UnitSystem;
            }
        }

        public static void SaveConfig()
        {
            if (GlobalData.IsSignIn)
            {
                UserConfigManager.OnceInstance.Language = GlobalData.Language.Value;
                UserConfigManager.OnceInstance.UnitSystem = GlobalData.Wireline.UnitSystem.Value;
                UserConfigManager.OnceInstance.SaveConfig();
            }
            else
            {
                SystemConfigManager.OnceInstance.Language = GlobalData.Language.Value;
                SystemConfigManager.OnceInstance.UnitSystem = GlobalData.Wireline.UnitSystem.Value;
                SystemConfigManager.OnceInstance.SaveConfig();
            }
        }
    }
}
