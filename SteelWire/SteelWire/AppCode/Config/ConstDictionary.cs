using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using BaseConfig;
using SteelWire.Business.Config;

namespace SteelWire.AppCode.Config
{
    /// <summary>
    /// 初始字典
    /// </summary>
    public static class ConstDictionary
    {
        /// <summary>
        /// 语言字典
        /// </summary>
        public static Dictionary<LanguageEnum, CultureInfo> LanguageDictionary { get; }
        /// <summary>
        /// 钢丝绳每米工作量
        /// </summary>
        public static List<WireropeWorkload> WireropeWorkloads { get; }
        /// <summary>
        /// 切绳长度
        /// </summary>
        public static List<WireropeCutRole> ConstWireropeCutRoles { get; }
        /// <summary>
        /// 缠绳效率
        /// </summary>
        public static List<WireropeEfficiency> ConstWireropeEfficiencies { get; }
        /// <summary>
        /// 钻机驱动方式
        /// </summary>
        public static List<DrillingType> ConstDrillingTypes { get; }

        static ConstDictionary()
        {
            LanguageDictionary = new Dictionary<LanguageEnum, CultureInfo>
            {
                {LanguageEnum.Default, null},
                {LanguageEnum.English, CultureInfo.GetCultureInfo("en-US")},
                {LanguageEnum.Chinese, CultureInfo.GetCultureInfo("zh-CN")}
            };
            WireropeWorkloads = new List<WireropeWorkload>
            {
                new WireropeWorkload
                {
                    Key = "InternationalSystem26.0",
                    UnitSystem = UnitSystemEnum.InternationalSystem,
                    Name = "26.0",
                    Diameter = 26.0M,
                    Workload = 34M
                },
                new WireropeWorkload
                {
                    Key = "InternationalSystem29.0",
                    UnitSystem = UnitSystemEnum.InternationalSystem,
                    Name = "29.0",
                    Diameter = 29.0M,
                    Workload = 48M
                },
                new WireropeWorkload
                {
                    Key = "InternationalSystem32.0",
                    UnitSystem = UnitSystemEnum.InternationalSystem,
                    Name = "32.0",
                    Diameter = 32.0M,
                    Workload = 67M
                },
                new WireropeWorkload
                {
                    Key = "InternationalSystem35.0",
                    UnitSystem = UnitSystemEnum.InternationalSystem,
                    Name = "35.0",
                    Diameter = 35.0M,
                    Workload = 86M
                },
                new WireropeWorkload
                {
                    Key = "InternationalSystem38.0",
                    UnitSystem = UnitSystemEnum.InternationalSystem,
                    Name = "38.0",
                    Diameter = 38.0M,
                    Workload = 100M
                },
                new WireropeWorkload
                {
                    Key = "InternationalSystem1",
                    UnitSystem = UnitSystemEnum.ImperialSystem,
                    Name = "1",
                    Diameter = 1M,
                    Workload = 7M
                },
                new WireropeWorkload
                {
                    Key = "InternationalSystem11/8",
                    UnitSystem = UnitSystemEnum.ImperialSystem,
                    Name = "11/8",
                    Diameter = 1.375M,
                    Workload = 10M
                },
                new WireropeWorkload
                {
                    Key = "InternationalSystem11/4",
                    UnitSystem = UnitSystemEnum.ImperialSystem,
                    Name = "11/4",
                    Diameter = 2.75M,
                    Workload = 14M
                },
                new WireropeWorkload
                {
                    Key = "InternationalSystem13/8",
                    UnitSystem = UnitSystemEnum.ImperialSystem,
                    Name = "13/8",
                    Diameter = 1.625M,
                    Workload = 18M
                },
                new WireropeWorkload
                {
                    Key = "InternationalSystem11/2",
                    UnitSystem = UnitSystemEnum.ImperialSystem,
                    Name = "11/2",
                    Diameter = 5.5M,
                    Workload = 21M
                }
            };
            ConstWireropeCutRoles = new List<WireropeCutRole>{
                new WireropeCutRole{MinDerrickHeight = 22M,MaxDerrickHeight = 27.9M,MinCutLength = 16M,MaxCutLength = 18M},
                new WireropeCutRole{MinDerrickHeight = 28M,MaxDerrickHeight = 36.5M,MinCutLength = 18M,MaxCutLength = 20M},
                new WireropeCutRole{MinDerrickHeight = 36.6M,MaxDerrickHeight = 40.4M,MinCutLength = 22M,MaxCutLength = 24M},
                new WireropeCutRole{MinDerrickHeight = 40.5M,MaxDerrickHeight = 42.9M,MinCutLength = 24M,MaxCutLength = 26M},
                new WireropeCutRole{MinDerrickHeight = 43M,MaxDerrickHeight = 46M,MinCutLength = 26M,MaxCutLength = 28M},
                new WireropeCutRole{MinDerrickHeight = 46M,MaxDerrickHeight = 0M,MinCutLength = 33M,MaxCutLength = 35M}
            };
            ConstWireropeEfficiencies = new List<WireropeEfficiency>
            {
                new WireropeEfficiency{Count = 6,SlidingValue = 0.748M,RollingValue = 0.874M},
                new WireropeEfficiency{Count = 8,SlidingValue = 0.692M,RollingValue = 0.842M},
                new WireropeEfficiency{Count = 10,SlidingValue = 0.642M,RollingValue = 0.811M},
                new WireropeEfficiency{Count = 12,SlidingValue = 0.597M,RollingValue = 0.782M},
                new WireropeEfficiency{Count = 14,SlidingValue = 0.556M,RollingValue = 0.755M}
            };
            ConstDrillingTypes = new List<DrillingType>
            {
                new DrillingType{Name = DrillingTypeEnum.TopDrive,Coefficient=1},
                new DrillingType{Name = DrillingTypeEnum.NotTopDrive,Coefficient=2}
            };
        }

        public static void InitializeConfigDictionary<T>(AddCollection<T> dictionary, IEnumerable<T> constItems)
            where T : ConfigurationElement, ISectionCollectionItem, new()
        {
            if (dictionary != null && constItems != null)
            {
                foreach (T constItem in constItems)
                {
                    dictionary.Add(constItem);
                }
            }
        }
    }
}