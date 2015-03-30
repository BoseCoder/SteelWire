using System.Collections.Generic;
using System.Configuration;
using BaseConfig;

namespace SteelWire.AppCode.Config
{
    public static class ConstDictionary
    {
        public static readonly List<WireropeWorkload> ConstWireropeWorkloads;
        public static readonly List<WireropeCutRole> ConstWireropeCutRoles;
        public static readonly List<WireropeEfficiency> ConstWireropeEfficiencies;
        public static readonly List<DrillingType> ConstDrillingTypes;
        public static readonly List<DrillingDifficulty> ConstDrillingDifficulties;

        static ConstDictionary()
        {
            ConstWireropeWorkloads = new List<WireropeWorkload>
            {
                new WireropeWorkload { Diameter = 26.0M, Workload = 34M },
                new WireropeWorkload { Diameter = 29.0M, Workload = 48M },
                new WireropeWorkload { Diameter = 32.0M, Workload = 67M },
                new WireropeWorkload { Diameter = 35.0M, Workload = 86M },
                new WireropeWorkload { Diameter = 38.0M, Workload = 96M },
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
                new DrillingType{Name = DrillingTypeEnum.TopDrive.ToString(),Coefficient=1},
                new DrillingType{Name = DrillingTypeEnum.ReamerAndTopDrive.ToString(),Coefficient=2},
                new DrillingType{Name = DrillingTypeEnum.NoRedressing.ToString(),Coefficient=2},
                new DrillingType{Name = DrillingTypeEnum.RedressingOnce.ToString(),Coefficient=3},
                new DrillingType{Name = DrillingTypeEnum.RedressingTwice.ToString(),Coefficient=4},
            };
            ConstDrillingDifficulties = new List<DrillingDifficulty>
            {
                new DrillingDifficulty{Name =DrillingDifficultyEnum.Easy.ToString(),Difficulty=1.0M},
                new DrillingDifficulty{Name = DrillingDifficultyEnum.Normal.ToString(),Difficulty=1.1M},
                new DrillingDifficulty{Name = DrillingDifficultyEnum.Hard.ToString(),Difficulty=1.2M},
                new DrillingDifficulty{Name = DrillingDifficultyEnum.Difficult.ToString(),Difficulty=1.3M},
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