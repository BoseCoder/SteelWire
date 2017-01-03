using SteelWire.AppCode.Config;
using SteelWire.AppCode.Data;

namespace SteelWire.WindowData
{
    public class Option
    {
        public void Save()
        {
            SystemConfigManager.OnceInstance.Language = GlobalData.Language.Value;
            SystemConfigManager.OnceInstance.UnitSystem = GlobalData.Wireline.UnitSystem.Value;
            SystemConfigManager.OnceInstance.SaveConfig();
        }
    }
}
