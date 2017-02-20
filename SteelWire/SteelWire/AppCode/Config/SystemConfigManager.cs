using BaseConfig;
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

        private SystemConfigManager(string key, string subFolder, string fileName)
            : base(key, subFolder, fileName)
        { }

        static SystemConfigManager()
        {
            InitializeConfig();
        }

        public static void InitializeConfig()
        {
            OnceInstance = new SystemConfigManager("UserConfig", "Config", "SystemConfig.config");
            OnceInstance.ReadConfig();

            if (!OnceInstance.AppConfig.HasFile)
            {
                OnceInstance.SaveConfig();
            }
        }
    }
}
