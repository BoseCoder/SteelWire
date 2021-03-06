﻿using BaseConfig;
using SteelWire.AppCode.Data;
using SteelWire.Business.Config;

namespace SteelWire.AppCode.Config
{
    /// <summary>
    /// 系统设置配置
    /// </summary>
    public class UserConfigManager : BaseConfigManager
    {
        public static UserConfigManager OnceInstance { get; private set; }

        [BaseConfigKey]
        public bool Run { get; set; }

        [BaseConfigKey]
        public LanguageEnum Language { get; set; }

        [BaseConfigKey]
        public UnitSystemEnum UnitSystem { get; set; }

        [BaseConfigKey]
        public string LastWirelineNumber { get; set; }

        private UserConfigManager(string key, string subFolder, string fileName)
            : base(key, subFolder, fileName)
        { }

        static UserConfigManager()
        {
            InitializeConfig(false);
        }

        public static void InitializeConfig(bool canWrite = true)
        {
            OnceInstance = new UserConfigManager("UserConfig", $"Config\\{GlobalData.Account}", "UserConfig.config");
            OnceInstance.ReadConfig();

            if (canWrite)
            {
                if (!OnceInstance.AppConfig.HasFile)
                {
                    OnceInstance.Language = SystemConfigManager.OnceInstance.Language;
                    OnceInstance.UnitSystem = SystemConfigManager.OnceInstance.UnitSystem;
                    OnceInstance.SaveConfig();
                }
            }
        }
    }
}
