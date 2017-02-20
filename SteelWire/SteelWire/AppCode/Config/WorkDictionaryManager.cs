using System.IO;
using BaseConfig;
using SteelWire.AppCode.Data;

namespace SteelWire.AppCode.Config
{
    /// <summary>
    /// 钢丝绳吨公里计算配置管理
    /// </summary>
    public class WorkDictionaryManager : BaseConfigManager
    {
        public static WorkDictionaryManager OnceInstance { get; private set; }

        /// <summary>
        /// 字典Section（钢丝绳每米工作量集合、钢丝绳切绳规则集合、缠绳效率集合）
        /// </summary>
        [BaseConfigSection]
        public WorkDictionary DictionarySection { get; set; }

        private WorkDictionaryManager(string key, string subFolder, string fileName)
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

        static WorkDictionaryManager()
        {
            InitializeConfig(false);
        }

        public static void InitializeConfig(bool canWrite = true)
        {
            OnceInstance = new WorkDictionaryManager("WorkDictionary", $"Config\\{GlobalData.Account}", "WorkDictionary.config");
            OnceInstance.ReadConfig();

            if (canWrite)
            {
                bool needWrite = false;

                #region DictionarySection

                WorkDictionary dictionary = OnceInstance.DictionarySection;
                if (dictionary == null)
                {
                    needWrite = true;
                    dictionary = new WorkDictionary();
                    OnceInstance.DictionarySection = dictionary;
                }
                if (dictionary.DrillingTypes.Count < 1)
                {
                    ConstDictionary.InitializeConfigDictionary(dictionary.DrillingTypes,
                        ConstDictionary.ConstDrillingTypes);
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
