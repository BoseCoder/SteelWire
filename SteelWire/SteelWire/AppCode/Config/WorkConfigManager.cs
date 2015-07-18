using System.IO;
using BaseConfig;
using System.Collections.Generic;
using System.Linq;
using SteelWire.WindowData;

namespace SteelWire.AppCode.Config
{
    /// <summary>
    /// 钢丝绳吨公里计算配置管理
    /// </summary>
    public class WorkConfigManager : BaseConfigManager
    {
        public static WorkConfigManager OnceInstance { get; private set; }

        /// <summary>
        /// 当前配置的各种参数集合
        /// </summary>
        [BaseConfigSection]
        public WorkConfig ConfigSection { get; set; }

        [BaseConfigKey]
        public string CurrentWireNo { get; set; }

        private WorkConfigManager(string key, string subFolder, string fileName)
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

        static WorkConfigManager()
        {
            InitializeConfig();
        }

        public static void InitializeConfig()
        {
            OnceInstance = new WorkConfigManager("WorkConfig",
                string.Format("Config\\{0}", Sign.Data.Account), "WorkConfig.config");
            OnceInstance.ReadConfig();
            WorkDictionary dictionary = WorkDictionaryManager.OnceInstance.DictionarySection;
            bool needWrite = false;

            #region ConfigSection

            WorkConfig current = OnceInstance.ConfigSection;
            if (current == null)
            {
                needWrite = true;
                current = new WorkConfig();
                OnceInstance.ConfigSection = current;
            }
            if (current.DrillPipes.Count < 1)
            {
                needWrite = true;
                current.DrillPipes.Add(new DrillPipe
                {
                    Name = "DrillPipe1"
                });
            }
            if (current.HeavierDrillPipes.Count < 1)
            {
                needWrite = true;
                current.HeavierDrillPipes.Add(new Drill
                {
                    Name = "HeavierDrillPipe1"
                });
            }
            if (current.DrillCollars.Count < 1)
            {
                needWrite = true;
                current.DrillCollars.Add(new Drill
                {
                    Name = "DrillCollar1"
                });
                current.DrillCollars.Add(new Drill
                {
                    Name = "DrillCollar2"
                });
            }
            IEnumerable<DrillingType> drillingTypes = dictionary.DrillingTypes.Cast<DrillingType>();
            if (drillingTypes.All(d => !Equals(d.Name, current.DrillingType)))
            {
                needWrite = true;
                current.DrillingType = drillingTypes.First().Name;
            }
            IEnumerable<DrillingDifficulty> drillingDifficulties = dictionary.DrillingDifficulties.Cast<DrillingDifficulty>();
            if (drillingDifficulties.All(d => !Equals(d.Name, current.DrillingDifficulty)))
            {
                needWrite = true;
                current.DrillingDifficulty = drillingDifficulties.First().Name;
            }

            #endregion

            if (needWrite)
            {
                OnceInstance.SaveConfig();
            }
        }
    }
}
