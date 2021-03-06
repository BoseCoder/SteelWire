﻿using System.IO;
using BaseConfig;
using SteelWire.AppCode.Data;

namespace SteelWire.AppCode.Config
{
    /// <summary>
    /// 切绳临界值计算配置管理
    /// </summary>
    public class CuttingCriticalDictionaryManager : BaseConfigManager
    {
        public static CuttingCriticalDictionaryManager OnceInstance { get; private set; }

        /// <summary>
        /// 字典Section（钢丝绳每米工作量集合、钢丝绳切绳规则集合、缠绳效率集合）
        /// </summary>
        [BaseConfigSection]
        public CuttingCriticalDictionary DictionarySection { get; set; }

        private CuttingCriticalDictionaryManager(string key, string subFolder, string fileName)
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

        static CuttingCriticalDictionaryManager()
        {
            InitializeConfig(false);
        }

        public static void InitializeConfig(bool canWrite = true)
        {
            OnceInstance = new CuttingCriticalDictionaryManager("CuttingCriticalDictionary",
                $"Config\\{GlobalData.Account}", "CuttingCriticalDictionary.config");
            OnceInstance.ReadConfig();

            if (canWrite)
            {
                bool needWrite = false;

                #region DictionarySectionInstance

                CuttingCriticalDictionary dictionary = OnceInstance.DictionarySection;
                if (dictionary == null)
                {
                    needWrite = true;
                    dictionary = new CuttingCriticalDictionary();
                    OnceInstance.DictionarySection = dictionary;
                }
                if (dictionary.WireropeWorkloads.Count < 1)
                {
                    needWrite = true;
                    ConstDictionary.InitializeConfigDictionary(dictionary.WireropeWorkloads,
                        ConstDictionary.WireropeWorkloads);
                }
                if (dictionary.WireropeCutRoles.Count < 1)
                {
                    needWrite = true;
                    ConstDictionary.InitializeConfigDictionary(dictionary.WireropeCutRoles,
                        ConstDictionary.ConstWireropeCutRoles);
                }
                if (dictionary.WireropeEfficiencies.Count < 1)
                {
                    needWrite = true;
                    ConstDictionary.InitializeConfigDictionary(dictionary.WireropeEfficiencies,
                        ConstDictionary.ConstWireropeEfficiencies);
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
