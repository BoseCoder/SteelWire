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
                    Key = "InternationalSystem8/8",
                    UnitSystem = UnitSystemEnum.ImperialSystem,
                    Name = "1",
                    Diameter = 1M,
                    Workload = 7M
                },
                new WireropeWorkload
                {
                    Key = "InternationalSystem9/8",
                    UnitSystem = UnitSystemEnum.ImperialSystem,
                    Name = "1-1/8",
                    Diameter = 1.125M,
                    Workload = 10M
                },
                new WireropeWorkload
                {
                    Key = "InternationalSystem10/8",
                    UnitSystem = UnitSystemEnum.ImperialSystem,
                    Name = "1-1/4",
                    Diameter = 1.25M,
                    Workload = 14M
                },
                new WireropeWorkload
                {
                    Key = "InternationalSystem11/8",
                    UnitSystem = UnitSystemEnum.ImperialSystem,
                    Name = "1-3/8",
                    Diameter = 1.375M,
                    Workload = 18M
                },
                new WireropeWorkload
                {
                    Key = "InternationalSystem12/8",
                    UnitSystem = UnitSystemEnum.ImperialSystem,
                    Name = "1-1/2",
                    Diameter = 1.5M,
                    Workload = 21M
                }
            };
            ConstWireropeCutRoles = new List<WireropeCutRole>
            {
                new WireropeCutRole
                {
                    UnitSystem = UnitSystemEnum.InternationalSystem,
                    MinDerrickHeight = 22M,
                    AllowMinDerrickHeight = true,
                    MaxDerrickHeight = 27.9M,
                    AllowMaxDerrickHeight = true,
                    MinCutLength = 17M,
                    MaxCutLength = 17M
                },
                new WireropeCutRole
                {
                    UnitSystem = UnitSystemEnum.InternationalSystem,
                    MinDerrickHeight = 27.9M,
                    AllowMinDerrickHeight = false,
                    MaxDerrickHeight = 36.5M,
                    AllowMaxDerrickHeight = true,
                    MinCutLength = 19M,
                    MaxCutLength = 19M
                },
                new WireropeCutRole
                {
                    UnitSystem = UnitSystemEnum.InternationalSystem,
                    MinDerrickHeight = 36.5M,
                    AllowMinDerrickHeight = false,
                    MaxDerrickHeight = 40.4M,
                    AllowMaxDerrickHeight = true,
                    MinCutLength = 23M,
                    MaxCutLength = 23M
                },
                new WireropeCutRole
                {
                    UnitSystem = UnitSystemEnum.InternationalSystem,
                    MinDerrickHeight = 40.4M,
                    AllowMinDerrickHeight = false,
                    MaxDerrickHeight = 42.9M,
                    AllowMaxDerrickHeight = true,
                    MinCutLength = 25M,
                    MaxCutLength = 25M
                },
                new WireropeCutRole
                {
                    UnitSystem = UnitSystemEnum.InternationalSystem,
                    MinDerrickHeight = 42.9M,
                    AllowMinDerrickHeight = false,
                    MaxDerrickHeight = 46M,
                    AllowMaxDerrickHeight = true,
                    MinCutLength = 27M,
                    MaxCutLength = 27M
                },
                new WireropeCutRole
                {
                    UnitSystem = UnitSystemEnum.InternationalSystem,
                    MinDerrickHeight = 46M,
                    AllowMinDerrickHeight = false,
                    MaxDerrickHeight = decimal.MaxValue,
                    AllowMaxDerrickHeight = true,
                    MinCutLength = 34M,
                    MaxCutLength = 34M
                },
                new WireropeCutRole
                {
                    UnitSystem = UnitSystemEnum.ImperialSystem,
                    MinDerrickHeight = 72M,
                    AllowMinDerrickHeight = true,
                    MaxDerrickHeight = 91.9M,
                    AllowMaxDerrickHeight = true,
                    MinCutLength = 56M,
                    MaxCutLength = 56M
                },
                new WireropeCutRole
                {
                    UnitSystem = UnitSystemEnum.ImperialSystem,
                    MinDerrickHeight = 91.9M,
                    AllowMinDerrickHeight = false,
                    MaxDerrickHeight = 119.9M,
                    AllowMaxDerrickHeight = true,
                    MinCutLength = 62M,
                    MaxCutLength = 62M
                },
                new WireropeCutRole
                {
                    UnitSystem = UnitSystemEnum.ImperialSystem,
                    MinDerrickHeight = 119.9M,
                    AllowMinDerrickHeight = false,
                    MaxDerrickHeight = 132.9M,
                    AllowMaxDerrickHeight = true,
                    MinCutLength = 76M,
                    MaxCutLength = 76M
                },
                new WireropeCutRole
                {
                    UnitSystem = UnitSystemEnum.ImperialSystem,
                    MinDerrickHeight = 132.9M,
                    AllowMinDerrickHeight = false,
                    MaxDerrickHeight = 140.9M,
                    AllowMaxDerrickHeight = true,
                    MinCutLength = 82M,
                    MaxCutLength = 82M
                },
                new WireropeCutRole
                {
                    UnitSystem = UnitSystemEnum.ImperialSystem,
                    MinDerrickHeight = 140.9M,
                    AllowMinDerrickHeight = false,
                    MaxDerrickHeight = 151M,
                    AllowMaxDerrickHeight = true,
                    MinCutLength = 89M,
                    MaxCutLength = 89M
                },
                new WireropeCutRole
                {
                    UnitSystem = UnitSystemEnum.ImperialSystem,
                    MinDerrickHeight = 151M,
                    AllowMinDerrickHeight = false,
                    MaxDerrickHeight = decimal.MaxValue,
                    AllowMaxDerrickHeight = true,
                    MinCutLength = 112M,
                    MaxCutLength = 112M
                },
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