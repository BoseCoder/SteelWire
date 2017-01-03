using BaseConfig;
using SteelWire.AppCode.Data;
using SteelWire.Business.Config;

namespace SteelWire.AppCode.Config
{
    /// <summary>
    /// 系统设置配置
    /// </summary>
    public class SystemConfigManager : BaseConfigManager
    {
        public static SystemConfigManager OnceInstance { get; private set; }

        [BaseConfigKey]
        public bool Run { get; set; }

        [BaseConfigKey]
        public LanguageEnum Language { get; set; }

        [BaseConfigKey]
        public UnitSystemEnum UnitSystem { get; set; }

        [BaseConfigKey]
        public string LastWirelineNumber { get; set; }

        [BaseConfigKey]
        public bool DataIsolation { get; set; }

        private SystemConfigManager(string key, string subFolder, string fileName)
            : base(key, subFolder, fileName)
        { }

        static SystemConfigManager()
        {
            InitializeConfig();
        }

        public static void InitializeConfig()
        {
            OnceInstance = new SystemConfigManager("SystemConfig", $"Config\\{GlobalData.Account}", "SystemConfig.config");
            OnceInstance.ReadConfig();
        }
    }
}
