using SteelWire.AppCode.Config;
using SteelWire.AppCode.Data;

namespace SteelWire.WindowData
{
    public class Option
    {
        public void Save()
        {
            UserConfigManager.OnceInstance.Language = GlobalData.Language.Value;
            UserConfigManager.OnceInstance.UnitSystem = GlobalData.Wireline.UnitSystem.Value;
            UserConfigManager.OnceInstance.SaveConfig();
        }
    }
}
