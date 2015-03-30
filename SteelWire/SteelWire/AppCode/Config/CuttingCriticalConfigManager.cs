using System.IO;
using BaseConfig;
using System.Collections.Generic;
using System.Linq;

namespace SteelWire.AppCode.Config
{
    /// <summary>
    /// 切绳临界值计算配置管理
    /// </summary>
    public class CuttingCriticalConfigManager : BaseConfigManager
    {
        public static readonly CuttingCriticalConfigManager OnceInstance;

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
            OnceInstance = new CuttingCriticalConfigManager("CuttingCriticalConfig", "Config", "CuttingCriticalConfig.config");
            OnceInstance.ReadConfig();
            CuttingCriticalDictionary dictionary = CuttingCriticalDictionaryManager.OnceInstance.DictionarySection;
            bool needWrite = false;
            #region ConfigSection
            CuttingCriticalConfig current = OnceInstance.ConfigSection;
            if (current == null)
            {
                needWrite = true;
                current = new CuttingCriticalConfig();
                OnceInstance.ConfigSection = current;
            }
            IEnumerable<WireropeWorkload> wireropeWorkloads = dictionary.WireropeWorkloads.Cast<WireropeWorkload>();
            if (wireropeWorkloads.All(w => w.Diameter != current.WirelineDiameter))
            {
                needWrite = true;
                current.WirelineDiameter = wireropeWorkloads.First().Diameter;
            }
            IEnumerable<WireropeEfficiency> wireropeEfficiencies = dictionary.WireropeEfficiencies.Cast<WireropeEfficiency>();
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
