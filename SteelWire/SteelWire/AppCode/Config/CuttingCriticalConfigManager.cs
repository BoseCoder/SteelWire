using System.IO;
using BaseConfig;
using System.Collections.Generic;
using System.Linq;
using SteelWire.AppCode.Data;

namespace SteelWire.AppCode.Config
{
    /// <summary>
    /// 切绳临界值计算配置管理
    /// </summary>
    public class CuttingCriticalConfigManager : BaseConfigManager
    {
        public static CuttingCriticalConfigManager OnceInstance { get; private set; }

        /// <summary>
        /// 当前配置的各种参数集合
        /// </summary>
        [BaseConfigSection]
        public CuttingCriticalConfig ConfigSection { get; set; }

        private CuttingCriticalConfigManager(string key, string subFolder, string fileName)
            : base(key, subFolder, fileName)
        { }

        /// <summary>
        /// 获取配置文件信息
        /// </summary>
        /// <returns></returns>
        public static FileInfo GetConfigFile()
        {
            if (OnceInstance.AppConfig.HasFile)
            {
                return new FileInfo(OnceInstance.AppConfig.FilePath);
            }
            return null;
        }

        static CuttingCriticalConfigManager()
        {
            InitializeConfig(false);
        }

        public static void InitializeConfig(bool canWrite = true)
        {
            OnceInstance = new CuttingCriticalConfigManager("CuttingCriticalConfig", $"Config\\{GlobalData.Account}", "CuttingCriticalConfig.config");
            OnceInstance.ReadConfig();

            if (canWrite)
            {
                bool needWrite = false;

                #region ConfigSection

                CuttingCriticalConfig current = OnceInstance.ConfigSection;
                if (current == null)
                {
                    needWrite = true;
                    current = new CuttingCriticalConfig();
                    OnceInstance.ConfigSection = current;
                }
                CuttingCriticalDictionary dictionary = CuttingCriticalDictionaryManager.OnceInstance.DictionarySection;
                List<WireropeEfficiency> wireropeEfficiencies =
                    dictionary.WireropeEfficiencies.Cast<WireropeEfficiency>().ToList();
                if (wireropeEfficiencies.All(w => w.Count != current.RopeCount))
                {
                    needWrite = true;
                    current.RopeCount = wireropeEfficiencies.First().Count;
                }

                #endregion

                if (needWrite)
                {
                    OnceInstance.SaveConfig();
                }
            }
        }
    }
}
