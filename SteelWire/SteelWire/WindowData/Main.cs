using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Transactions;
using BaseConfig;
using SteelWire.AppCode.Config;
using SteelWire.AppCode.CustomException;
using SteelWire.AppCode.Dependencies;
using SteelWire.Business.CalculateCommander;
using SteelWire.Business.Database;
using SteelWire.Business.DbOperator;
using WireropeWorkload = SteelWire.AppCode.Config.WireropeWorkload;
using WireropeCutRole = SteelWire.AppCode.Config.WireropeCutRole;
using WireropeEfficiency = SteelWire.AppCode.Config.WireropeEfficiency;
using CuttingCriticalDictionary = SteelWire.AppCode.Config.CuttingCriticalDictionary;
using CuttingCriticalConfig = SteelWire.AppCode.Config.CuttingCriticalConfig;

using DrillingDifficulty = SteelWire.AppCode.Config.DrillingDifficulty;
using DrillingType = SteelWire.AppCode.Config.DrillingType;
using WorkDictionary = SteelWire.AppCode.Config.WorkDictionary;
using DrillPipeConfig = SteelWire.AppCode.Dependencies.DrillPipeConfig;
using DrillCollarConfig = SteelWire.AppCode.Dependencies.DrillCollarConfig;
using WorkConfig = SteelWire.AppCode.Config.WorkConfig;

using DbWireropeWorkload = SteelWire.Business.Database.WireropeWorkload;
using DbWireropeCutRole = SteelWire.Business.Database.WireropeCutRole;
using DbWireropeEfficiency = SteelWire.Business.Database.WireropeEfficiency;
using DbCuttingCriticalDictionary = SteelWire.Business.Database.CuttingCriticalDictionary;
using DbCuttingCriticalConfig = SteelWire.Business.Database.CuttingCriticalConfig;

using DbDrillingType = SteelWire.Business.Database.DrillingType;
using DbDrillingDifficulty = SteelWire.Business.Database.DrillingDifficulty;
using DbWorkDictionary = SteelWire.Business.Database.WorkDictionary;
using DbWorkConfig = SteelWire.Business.Database.WorkConfig;
using DbDrillPipeConfig = SteelWire.Business.Database.DrillPipeConfig;
using DbDrillCollarConfig = SteelWire.Business.Database.DrillCollarConfig;

namespace SteelWire.WindowData
{
    public class Main
    {
        private static readonly CuttingCriticalDictionary CuttingCriticalDic;
        private static CuttingCriticalConfig _cuttingCriticalConf;
        private static readonly WorkDictionary WorkDic;
        private static WorkConfig _workConf;
        public static readonly Main Data;

        static Main()
        {
            CuttingCriticalDic = CuttingCriticalDictionaryManager.OnceInstance.DictionarySection;
            WorkDic = WorkDictionaryManager.OnceInstance.DictionarySection;

            _cuttingCriticalConf = CuttingCriticalConfigManager.OnceInstance.ConfigSection;
            _workConf = WorkConfigManager.OnceInstance.ConfigSection;

            Data = new Main
            {
                // 1
                DerrickHeight = new DependencyItem<decimal>
                {
                    ItemValue = _cuttingCriticalConf.DerrickHeight,
                },
                WirelineMaxPower = new DependencyItem<decimal>
                {
                    ItemValue = _cuttingCriticalConf.WirelineMaxPower
                },
                RotaryHookWorkload = new DependencyItem<decimal>
                {
                    ItemValue = _cuttingCriticalConf.RotaryHookWorkload
                },
                RollerDiameter = new DependencyItem<decimal>
                {
                    ItemValue = _cuttingCriticalConf.RollerDiameter
                },
                WirelineDiameter = new DependencyItem<decimal>
                {
                    ItemValue = _cuttingCriticalConf.WirelineDiameter
                },
                RopeCount = new DependencyItem<int>
                {
                    ItemValue = _cuttingCriticalConf.RopeCount
                },
                WirelineCuttingCriticalValue = new DependencyItem<decimal>(),

                // 2
                FluidDensity = new DependencyItem<decimal>
                {
                    ItemValue = _workConf.FluidDensity
                },
                ElevatorWeight = new DependencyItem<decimal>
                {
                    ItemValue = _workConf.ElevatorWeight
                },
                DrillPipes = new DependencyDrillCollection<DrillPipeConfig>(),
                DrillCollars = new DependencyDrillCollection<DrillCollarConfig>(),

                // 2-1
                DrillingShallowHeight = new DependencyItem<decimal>
                {
                    ItemValue = _workConf.DrillingShallowHeight
                },
                DrillingDeepHeight = new DependencyItem<decimal>
                {
                    ItemValue = _workConf.DrillingDeepHeight
                },
                DrillingType = new DependencyItem<string>
                {
                    ItemValue = _workConf.DrillingType
                },
                DrillingDifficulty = new DependencyItem<string>
                {
                    ItemValue = _workConf.DrillingDifficulty
                },
                DrillingWorkValue = new DependencyItem<decimal>(),

                // 2-2
                TripShallowHeight = new DependencyItem<decimal>
                {
                    ItemValue = _workConf.TripShallowHeight
                },
                TripDeepHeight = new DependencyItem<decimal>
                {
                    ItemValue = _workConf.TripDeepHeight
                },
                TripCount = new DependencyItem<int>
                {
                    ItemValue = _workConf.TripCount
                },
                TripWorkValue = new DependencyItem<decimal>(),

                // 2-3
                BushingWeight = new DependencyItem<decimal>
                {
                    ItemValue = _workConf.BushingWeight
                },
                BushingLength = new DependencyItem<decimal>
                {
                    ItemValue = _workConf.BushingLength
                },
                BushingHeight = new DependencyItem<decimal>
                {
                    ItemValue = _workConf.BushingHeight
                },
                BushingWorkValue = new DependencyItem<decimal>(),

                // 2-4
                CoringShallowHeight = new DependencyItem<decimal>
                {
                    ItemValue = _workConf.CoringShallowHeight
                },
                CoringDeepHeight = new DependencyItem<decimal>
                {
                    ItemValue = _workConf.CoringDeepHeight
                },
                CoringWorkValue = new DependencyItem<decimal>(),

                TotalWorkValue = new DependencyItem<decimal>(),

                UserDisplay = Sign.Data.UserDisplay,
                CriticalValue = new DependencyItem<decimal>(),
                CumulationValue = new DependencyItem<decimal>(),
                Log = new DependencyItem<string>()
            };

            Data.DrillPipes.AddRange(_workConf.DrillPipes.Cast<Drill>()
                .Select(d => new DrillPipeConfig
                {
                    Name = new DependencyItem<string>
                    {
                        ItemValue = d.Name
                    },
                    Weight = new DependencyItem<decimal>
                    {
                        ItemValue = d.Weight
                    },
                    Length = new DependencyItem<decimal>
                    {
                        ItemValue = d.Length
                    }
                }));

            Data.DrillCollars.AddRange(_workConf.DrillCollars.Cast<Drill>()
                .Select(d => new DrillCollarConfig
                {
                    Name = new DependencyItem<string>
                    {
                        ItemValue = d.Name
                    },
                    Weight = new DependencyItem<decimal>
                    {
                        ItemValue = d.Weight
                    },
                    Length = new DependencyItem<decimal>
                    {
                        ItemValue = d.Length
                    },

                }));

            //OnceInstance.DerrickHeight.ItemValueChangedHandler += WirelineCuttingCriticalValueChanged;
            Data.WirelineMaxPower.ItemValueChangedHandler += WirelineCuttingCriticalValueChanged;
            Data.RotaryHookWorkload.ItemValueChangedHandler += WirelineCuttingCriticalValueChanged;
            Data.RollerDiameter.ItemValueChangedHandler += WirelineCuttingCriticalValueChanged;
            Data.WirelineDiameter.ItemValueChangedHandler += WirelineCuttingCriticalValueChanged;
            Data.RopeCount.ItemValueChangedHandler += WirelineCuttingCriticalValueChanged;

            Data.FluidDensity.ItemValueChangedHandler += CommonCoefficientValueChanged;
            Data.ElevatorWeight.ItemValueChangedHandler += CommonCoefficientValueChanged;
            Data.DrillPipes.ItemsChangedHandler += CommonCoefficientValueChanged;
            Data.DrillCollars.ItemsChangedHandler += CommonCoefficientValueChanged;

            Data.DrillingShallowHeight.ItemValueChangedHandler += DrillingWorkValueChanged;
            Data.DrillingDeepHeight.ItemValueChangedHandler += DrillingWorkValueChanged;
            Data.DrillingType.ItemValueChangedHandler += DrillingWorkValueChanged;
            Data.DrillingDifficulty.ItemValueChangedHandler += DrillingWorkValueChanged;

            Data.TripDeepHeight.ItemValueChangedHandler += TripWorkValueChanged;
            Data.TripShallowHeight.ItemValueChangedHandler += TripWorkValueChanged;
            Data.TripCount.ItemValueChangedHandler += TripWorkValueChanged;

            Data.BushingWeight.ItemValueChangedHandler += BushingWorkValueChanged;
            Data.BushingLength.ItemValueChangedHandler += BushingWorkValueChanged;
            Data.BushingHeight.ItemValueChangedHandler += BushingWorkValueChanged;

            Data.CoringShallowHeight.ItemValueChangedHandler += CoringWorkValueChanged;
            Data.CoringDeepHeight.ItemValueChangedHandler += CoringWorkValueChanged;

            CalculateWirelineCuttingCritical();
            CommanderTripOnce commanderOnce = CreateCommanderOnce();
            CalculateDrillingWork(commanderOnce);
            CalculateTripWork(commanderOnce);
            CalculateBushingWork();
            CalculateCoringWork(commanderOnce);
            CalculateTotalWork();
        }

        private static void WirelineCuttingCriticalValueChanged(object sender, EventArgs e)
        {
            CalculateWirelineCuttingCritical();
        }

        private static void CommonCoefficientValueChanged(object sender, EventArgs e)
        {
            CommanderTripOnce commanderOnce = CreateCommanderOnce();
            CalculateDrillingWork(commanderOnce);
            CalculateTripWork(commanderOnce);
            CalculateBushingWork();
            CalculateCoringWork(commanderOnce);
            CalculateTotalWork();
        }

        private static void DrillingWorkValueChanged(object sender, EventArgs e)
        {
            CommanderTripOnce commanderOnce = CreateCommanderOnce();
            CalculateDrillingWork(commanderOnce);
            CalculateTotalWork();
        }

        private static void TripWorkValueChanged(object sender, EventArgs e)
        {
            CommanderTripOnce commanderOnce = CreateCommanderOnce();
            CalculateTripWork(commanderOnce);
            CalculateTotalWork();
        }

        private static void BushingWorkValueChanged(object sender, EventArgs e)
        {
            CalculateBushingWork();
            CalculateTotalWork();
        }

        private static void CoringWorkValueChanged(object sender, EventArgs e)
        {
            CommanderTripOnce commanderOnce = CreateCommanderOnce();
            CalculateCoringWork(commanderOnce);
            CalculateTotalWork();
        }

        private static CommanderTripOnce CreateCommanderOnce()
        {
            return new CommanderTripOnce
            {
                FluidDensity = Data.FluidDensity.ItemValue,
                ElevatorWeight = Data.ElevatorWeight.ItemValue,
                DrillPipeWeight = Data.DrillPipes.AverageWeight.ItemValue,
                DrillPipeLength = Data.DrillPipes.TotalLength.ItemValue,
                DrillCollarWeight = Data.DrillCollars.AverageWeight.ItemValue,
                DrillCollarLength = Data.DrillCollars.TotalLength.ItemValue
            };
        }

        private static void CalculateWirelineCuttingCritical()
        {
            WireropeWorkload wireropeWorkload = CuttingCriticalDic.WireropeWorkloads[Data.WirelineDiameter.ItemValue];
            if (wireropeWorkload == null)
            {
                Data.WirelineCuttingCriticalValue.ItemValue = -1;
                return;
            }
            WireropeEfficiency wireropeEfficiency = CuttingCriticalDic.WireropeEfficiencies[Data.RopeCount.ItemValue];
            if (wireropeEfficiency == null)
            {
                Data.WirelineCuttingCriticalValue.ItemValue = -1;
                return;
            }
            CommanderRopeCut commander = new CommanderRopeCut
            {
                WirelineWorkloadPerMetre = wireropeWorkload.Workload,
                WirelineMaxPower = Data.WirelineMaxPower.ItemValue,
                RotaryHookWorkload = Data.RotaryHookWorkload.ItemValue,
                RopeEfficiency = wireropeEfficiency.RollingValue,
                RopeCount = Data.RopeCount.ItemValue,
                RollerDiameter = Data.RollerDiameter.ItemValue,
                WirelineDiameter = Data.WirelineDiameter.ItemValue
            };
            try
            {
                Data.WirelineCuttingCriticalValue.ItemValue = commander.CalculateValue();
            }
            catch (Exception)
            {
                Data.WirelineCuttingCriticalValue.ItemValue = -1;
            }
        }

        private static void CalculateDrillingWork(CommanderTripOnce commanderOnce)
        {
            DrillingType type = WorkDic.DrillingTypes[Data.DrillingType.ItemValue];
            if (type == null)
            {
                Data.DrillingWorkValue.ItemValue = -1;
                return;
            }
            DrillingDifficulty difficulty = WorkDic.DrillingDifficulties[Data.DrillingDifficulty.ItemValue];
            if (difficulty == null)
            {
                Data.DrillingWorkValue.ItemValue = -1;
                return;
            }
            CommanderDrilling commander = new CommanderDrilling(commanderOnce)
            {
                Type = type.Coefficient,
                Difficulty = difficulty.Difficulty,
                DeepHeight = Data.DrillingDeepHeight.ItemValue,
                ShallowHeight = Data.DrillingShallowHeight.ItemValue
            };
            try
            {
                Data.DrillingWorkValue.ItemValue = commander.CalculateValue();
            }
            catch (Exception)
            {
                Data.DrillingWorkValue.ItemValue = -1;
            }
        }

        private static void CalculateTripWork(CommanderTripOnce commanderOnce)
        {
            CommanderTrip commander = new CommanderTrip(commanderOnce)
            {
                DeepHeight = Data.TripDeepHeight.ItemValue,
                ShallowHeight = Data.TripShallowHeight.ItemValue,
                Count = Data.TripCount.ItemValue
            };
            try
            {
                Data.TripWorkValue.ItemValue = commander.CalculateValue();
            }
            catch (Exception)
            {
                Data.TripWorkValue.ItemValue = -1;
            }
        }

        private static void CalculateBushingWork()
        {
            CommanderBushing commander = new CommanderBushing
            {
                FluidDensity = Data.FluidDensity.ItemValue,
                ElevatorWeight = Data.ElevatorWeight.ItemValue,
                BushingWeight = Data.BushingWeight.ItemValue,
                BushingLength = Data.BushingLength.ItemValue,
                BushingHeight = Data.BushingHeight.ItemValue
            };
            try
            {
                Data.BushingWorkValue.ItemValue = commander.CalculateValue();
            }
            catch (Exception)
            {
                Data.BushingWorkValue.ItemValue = -1;
            }
        }

        private static void CalculateCoringWork(CommanderTripOnce commanderOnce)
        {
            CommanderCoring commander = new CommanderCoring(commanderOnce)
            {
                DeepHeight = Data.CoringDeepHeight.ItemValue,
                ShallowHeight = Data.CoringShallowHeight.ItemValue
            };
            try
            {
                Data.CoringWorkValue.ItemValue = commander.CalculateValue();
            }
            catch (Exception)
            {
                Data.CoringWorkValue.ItemValue = -1;
            }
        }

        private static void CalculateTotalWork()
        {
            if (Data.DrillingWorkValue.ItemValue < 0
                || Data.TripWorkValue.ItemValue < 0
                || Data.BushingWorkValue.ItemValue < 0
                || Data.CoringWorkValue.ItemValue < 0)
            {
                Data.TotalWorkValue.ItemValue = -1;
                return;
            }
            Data.TotalWorkValue.ItemValue = Data.DrillingWorkValue.ItemValue
                + Data.TripWorkValue.ItemValue
                + Data.BushingWorkValue.ItemValue
                + Data.CoringWorkValue.ItemValue;
        }

        private Main()
        {

        }

        public DependencyItem<decimal> DerrickHeight { get; private set; }
        public DependencyItem<decimal> WirelineMaxPower { get; private set; }
        public DependencyItem<decimal> RotaryHookWorkload { get; private set; }
        public DependencyItem<decimal> RollerDiameter { get; private set; }
        public DependencyItem<decimal> WirelineDiameter { get; private set; }
        public DependencyItem<int> RopeCount { get; private set; }
        public DependencyItem<decimal> WirelineCuttingCriticalValue { get; private set; }

        public DependencyItem<decimal> FluidDensity { get; private set; }
        public DependencyItem<decimal> ElevatorWeight { get; private set; }
        public DependencyDrillCollection<DrillPipeConfig> DrillPipes { get; private set; }
        public DependencyDrillCollection<DrillCollarConfig> DrillCollars { get; private set; }

        public DependencyItem<decimal> DrillingShallowHeight { get; private set; }
        public DependencyItem<decimal> DrillingDeepHeight { get; private set; }
        public DependencyItem<string> DrillingType { get; private set; }
        public DependencyItem<string> DrillingDifficulty { get; private set; }
        public DependencyItem<decimal> DrillingWorkValue { get; private set; }

        public DependencyItem<decimal> TripShallowHeight { get; private set; }
        public DependencyItem<decimal> TripDeepHeight { get; private set; }
        public DependencyItem<int> TripCount { get; private set; }
        public DependencyItem<decimal> TripWorkValue { get; private set; }

        public DependencyItem<decimal> BushingWeight { get; private set; }
        public DependencyItem<decimal> BushingLength { get; private set; }
        public DependencyItem<decimal> BushingHeight { get; private set; }
        public DependencyItem<decimal> BushingWorkValue { get; private set; }

        public DependencyItem<decimal> CoringShallowHeight { get; private set; }
        public DependencyItem<decimal> CoringDeepHeight { get; private set; }
        public DependencyItem<decimal> CoringWorkValue { get; private set; }

        public DependencyItem<decimal> TotalWorkValue { get; private set; }

        public DependencyItem<string> UserDisplay { get; private set; }
        public DependencyItem<decimal> CriticalValue { get; private set; }
        public DependencyItem<decimal> CumulationValue { get; private set; }
        public DependencyItem<string> Log { get; private set; }

        public void RefreshData()
        {
            CumulationReset data = ResetOperator.GetCurrentData(Sign.Data.UserID);
            if (data != null)
            {
                this.CriticalValue.ItemValue = data.CriticalValue;
                this.CumulationValue.ItemValue = data.CumulationValue;
            }
        }

        public void RefreshData(SteelWireContext dbContext)
        {
            CumulationReset data = ResetOperator.GetCurrentData(dbContext, Sign.Data.UserID);
            if (data != null)
            {
                this.CriticalValue.ItemValue = data.CriticalValue;
                this.CumulationValue.ItemValue = data.CumulationValue;
            }
        }

        public void DownLoadConfig()
        {
            // 数据库下载配置信息功能未实现
        }

        public void Cumulate()
        {
            DateTime now = DateTime.Now;
            using (SteelWireContext dbContext = new SteelWireContext())
            {
                using (TransactionScope t = new TransactionScope())
                {
                    this.UpdateCritical(dbContext, now);
                    dbContext.SaveChanges();
                    this.UpdateWork(dbContext, now);
                    dbContext.SaveChanges();
                    this.RefreshData(dbContext);
                    t.Complete();
                }
            }
        }

        private DateTime GetLastWriteTime(DateTime time)
        {
            return new DateTime(time.Ticks - (time.Ticks % TimeSpan.TicksPerMillisecond), time.Kind);
        }

        private bool ChangeToCriticalConfig()
        {
            CuttingCriticalConfigManager.OnceInstance.ReloadConfig();
            _cuttingCriticalConf = CuttingCriticalConfigManager.OnceInstance.ConfigSection;
            bool changed = false;
            if (_cuttingCriticalConf.DerrickHeight != DerrickHeight.ItemValue)
            {
                _cuttingCriticalConf.DerrickHeight = DerrickHeight.ItemValue;
                changed = true;
            }
            if (_cuttingCriticalConf.WirelineMaxPower != WirelineMaxPower.ItemValue)
            {
                _cuttingCriticalConf.WirelineMaxPower = WirelineMaxPower.ItemValue;
                changed = true;
            }
            if (_cuttingCriticalConf.RotaryHookWorkload != RotaryHookWorkload.ItemValue)
            {
                _cuttingCriticalConf.RotaryHookWorkload = RotaryHookWorkload.ItemValue;
                changed = true;
            }
            if (_cuttingCriticalConf.RollerDiameter != RollerDiameter.ItemValue)
            {
                _cuttingCriticalConf.RollerDiameter = RollerDiameter.ItemValue;
                changed = true;
            }
            if (_cuttingCriticalConf.WirelineDiameter != WirelineDiameter.ItemValue)
            {
                _cuttingCriticalConf.WirelineDiameter = WirelineDiameter.ItemValue;
                changed = true;
            }
            if (_cuttingCriticalConf.RopeCount != RopeCount.ItemValue)
            {
                _cuttingCriticalConf.RopeCount = RopeCount.ItemValue;
                changed = true;
            }
            if (changed)
            {
                CuttingCriticalConfigManager.OnceInstance.SaveConfig();
            }
            return changed;
        }

        public void UpdateCritical(SteelWireContext dbContext, DateTime now)
        {
            now = GetLastWriteTime(now);
            if (this.WirelineCuttingCriticalValue.ItemValue <= 0)
            {
                throw new ErrorException("CriticalValueInvalid");
            }
            FileInfo dicInfo = CuttingCriticalDictionaryManager.GetConfigFile();
            if (dicInfo == null)
            {
                throw new ErrorException("CriticalDictionaryFileNotFound");
            }
            FileInfo configInfo = CuttingCriticalConfigManager.GetConfigFile();
            if (configInfo == null)
            {
                throw new ErrorException("CriticalConfigFileNotFound");
            }
            bool overWrite;
            DbCuttingCriticalDictionary dicData;
            bool dicUpdate = CriticalOperator.IsNeedUpdateDictionary(dbContext, Sign.Data.UserID,
                GetLastWriteTime(dicInfo.LastWriteTime), out overWrite, out dicData);
            if (dicUpdate)
            {
                if (overWrite)
                {
                    dicInfo.LastWriteTime = now;
                }
                CuttingCriticalDictionary dictionary = CuttingCriticalDictionaryManager.OnceInstance.DictionarySection;
                dicData = new DbCuttingCriticalDictionary
                {
                    ConfigUserID = Sign.Data.UserID,
                    ConfigTime = dicInfo.LastWriteTime,
                    WireropeWorkload = dictionary.WireropeWorkloads.Cast<WireropeWorkload>()
                        .Select(w => new DbWireropeWorkload
                        {
                            Diameter = w.Diameter,
                            Workload = w.Workload
                        }).ToList(),
                    WireropeCutRole = dictionary.WireropeCutRoles.Cast<WireropeCutRole>()
                        .Select(w => new DbWireropeCutRole
                        {
                            Key = w.ID,
                            MinDerrickHeight = w.MinDerrickHeight,
                            MaxDerrickHeight = w.MaxDerrickHeight,
                            MinCutLength = w.MinCutLength,
                            MaxCutLength = w.MaxCutLength
                        }).ToList(),
                    WireropeEfficiency = dictionary.WireropeEfficiencies.Cast<WireropeEfficiency>()
                        .Select(w => new DbWireropeEfficiency
                        {
                            RopeCount = w.Count,
                            SlidingValue = w.SlidingValue,
                            RollingValue = w.RollingValue
                        }).ToList()
                };
                CriticalOperator.UpdateDictionary(dbContext, dicData);
            }
            decimal criticalValue = Math.Round(this.WirelineCuttingCriticalValue.ItemValue, 8);
            bool configChanged = this.ChangeToCriticalConfig();
            if (!configChanged)
            {
                configChanged = CriticalOperator.IsNeedUpdateCritical(dbContext, Sign.Data.UserID,
                    GetLastWriteTime(configInfo.LastWriteTime), criticalValue, out overWrite);
            }
            if (dicUpdate || configChanged)
            {
                configInfo.LastWriteTime = now;
                DbCuttingCriticalConfig configData = new DbCuttingCriticalConfig
                {
                    DictionaryID = dicData.ID,
                    ConfigUserID = Sign.Data.UserID,
                    DerrickHeight = this.DerrickHeight.ItemValue,
                    WirelineMaxPower = this.WirelineMaxPower.ItemValue,
                    RotaryHookWorkload = this.RotaryHookWorkload.ItemValue,
                    RollerDiameter = this.RollerDiameter.ItemValue,
                    WirelineDiameter = this.WirelineDiameter.ItemValue,
                    RopeCount = this.RopeCount.ItemValue,
                    CuttingCriticalValue = criticalValue,
                    ConfigTime = configInfo.LastWriteTime
                };
                CriticalOperator.UpdateCriticalValue(dbContext, Sign.Data.UserID, configData);
            }
        }

        private bool ChangeDrillList<T>(AddCollection<Drill> drills, List<T> configs)
            where T : DrillConfig
        {
            bool changed = false;
            IEnumerable<Drill> remove = drills.Cast<Drill>().Where(d => !configs.Exists(p => p.Name.ItemValue == d.Name));
            if (remove.Any())
            {
                foreach (Drill drill in remove)
                {
                    drills.Remove(drill);
                }
                changed = true;
            }
            foreach (T config in configs)
            {
                Drill item = drills[config.Name.ItemValue];
                if (item == null)
                {
                    item = new Drill
                    {
                        Name = config.Name.ItemValue,
                        Weight = config.Weight.ItemValue,
                        Length = config.Length.ItemValue
                    };
                    drills.Add(item);
                    changed = true;
                }
                else
                {
                    if (item.Weight != config.Weight.ItemValue)
                    {
                        item.Weight = config.Weight.ItemValue;
                        changed = true;
                    }
                    if (item.Length != config.Length.ItemValue)
                    {
                        item.Length = config.Length.ItemValue;
                        changed = true;
                    }
                }
            }
            return changed;
        }

        public bool ChangeToWorkConfig()
        {
            WorkConfigManager.OnceInstance.ReloadConfig();
            _workConf = WorkConfigManager.OnceInstance.ConfigSection;
            bool changed = false;

            #region Common
            if (_workConf.FluidDensity != FluidDensity.ItemValue)
            {
                _workConf.FluidDensity = FluidDensity.ItemValue;
                changed = true;
            }
            if (_workConf.ElevatorWeight != ElevatorWeight.ItemValue)
            {
                _workConf.ElevatorWeight = ElevatorWeight.ItemValue;
                changed = true;
            }
            if (ChangeDrillList(_workConf.DrillPipes, DrillPipes.Items))
            {
                changed = true;
            }
            if (ChangeDrillList(_workConf.DrillCollars, DrillCollars.Items))
            {
                changed = true;
            }
            #endregion

            #region Drilling
            if (_workConf.DrillingShallowHeight != DrillingShallowHeight.ItemValue)
            {
                _workConf.DrillingShallowHeight = DrillingShallowHeight.ItemValue;
                changed = true;
            }
            if (_workConf.DrillingDeepHeight != DrillingDeepHeight.ItemValue)
            {
                _workConf.DrillingDeepHeight = DrillingDeepHeight.ItemValue;
                changed = true;
            }
            if (_workConf.DrillingType != DrillingType.ItemValue)
            {
                _workConf.DrillingType = DrillingType.ItemValue;
                changed = true;
            }
            if (_workConf.DrillingDifficulty != DrillingDifficulty.ItemValue)
            {
                _workConf.DrillingDifficulty = DrillingDifficulty.ItemValue;
                changed = true;
            }
            #endregion

            #region Trip
            if (_workConf.TripShallowHeight != TripShallowHeight.ItemValue)
            {
                _workConf.TripShallowHeight = TripShallowHeight.ItemValue;
                changed = true;
            }
            if (_workConf.TripDeepHeight != TripDeepHeight.ItemValue)
            {
                _workConf.TripDeepHeight = TripDeepHeight.ItemValue;
                changed = true;
            }
            if (_workConf.TripCount != TripCount.ItemValue)
            {
                _workConf.TripCount = TripCount.ItemValue;
                changed = true;
            }
            #endregion

            #region Bushing
            if (_workConf.BushingWeight != BushingWeight.ItemValue)
            {
                _workConf.BushingWeight = BushingWeight.ItemValue;
                changed = true;
            }
            if (_workConf.BushingLength != BushingLength.ItemValue)
            {
                _workConf.BushingLength = BushingLength.ItemValue;
                changed = true;
            }
            if (_workConf.BushingHeight != BushingHeight.ItemValue)
            {
                _workConf.BushingHeight = BushingHeight.ItemValue;
                changed = true;
            }
            #endregion

            #region Coring
            if (_workConf.CoringShallowHeight != CoringShallowHeight.ItemValue)
            {
                _workConf.CoringShallowHeight = CoringShallowHeight.ItemValue;
                changed = true;
            }
            if (_workConf.CoringDeepHeight != CoringDeepHeight.ItemValue)
            {
                _workConf.CoringDeepHeight = CoringDeepHeight.ItemValue;
                changed = true;
            }
            #endregion

            if (changed)
            {
                WorkConfigManager.OnceInstance.SaveConfig();
            }
            return changed;
        }

        public void UpdateWork(SteelWireContext dbContext, DateTime now)
        {
            //now = GetLastWriteTime(now);
            if (this.TotalWorkValue.ItemValue <= 0)
            {
                throw new ErrorException("CumulationValueInvalid");
            }
            FileInfo dicInfo = WorkDictionaryManager.GetConfigFile();
            if (dicInfo == null)
            {
                throw new ErrorException("CumulationDictionaryFileNotFound");
            }
            FileInfo configInfo = WorkConfigManager.GetConfigFile();
            if (configInfo == null)
            {
                throw new ErrorException("CumulationConfigFileNotFound");
            }
            if (ResetOperator.GetCurrentData(dbContext, Sign.Data.UserID) == null)
            {
                throw new ErrorException("CumulationDataNotFound");
            }
            bool overWrite;
            DbWorkDictionary dicData;
            bool dicUpdate = WorkOperator.IsNeedUpdateDictionary(dbContext, Sign.Data.UserID,
                GetLastWriteTime(dicInfo.LastWriteTime), out overWrite, out dicData);
            if (dicUpdate)
            {
                if (overWrite)
                {
                    dicInfo.LastWriteTime = now;
                }
                WorkDictionary dictionary = WorkDictionaryManager.OnceInstance.DictionarySection;
                dicData = new DbWorkDictionary
                {
                    ConfigUserID = Sign.Data.UserID,
                    ConfigTime = dicInfo.LastWriteTime,
                    DrillingType = dictionary.DrillingTypes.Cast<DrillingType>()
                        .Select(d => new DbDrillingType
                        {
                            DrillingTypeName = d.Name,
                            Coefficient = d.Coefficient
                        }).ToList(),
                    DrillingDifficulty = dictionary.DrillingDifficulties.Cast<DrillingDifficulty>()
                        .Select(d => new DbDrillingDifficulty
                        {
                            DrillingDifficultyName = d.Name,
                            DrillingDifficultyValue = d.Difficulty
                        }).ToList()
                };
                WorkOperator.UpdateDictionary(dbContext, dicData);
            }
            this.ChangeToWorkConfig();
            configInfo.LastWriteTime = now;
            DbWorkConfig workData = new DbWorkConfig
            {
                ConfigUserID = Sign.Data.UserID,
                DictionaryID = dicData.ID,
                FluidDensity = this.FluidDensity.ItemValue,
                ElevatorWeight = this.ElevatorWeight.ItemValue,
                DrillPipeConfig = this.DrillPipes.Items
                    .Where(d => d.Weight.ItemValue > 0 && d.Length.ItemValue > 0)
                    .Select(d => new DbDrillPipeConfig
                    {
                        DrillPipeName = d.Name.ItemValue,
                        DrillPipeWeight = d.Weight.ItemValue,
                        DrillPipeLength = d.Length.ItemValue
                    }).ToList(),
                DrillCollarConfig = this.DrillCollars.Items
                    .Where(d => d.Weight.ItemValue > 0 && d.Length.ItemValue > 0)
                    .Select(d => new DbDrillCollarConfig
                    {
                        DrillCollarName = d.Name.ItemValue,
                        DrillCollarWeight = d.Weight.ItemValue,
                        DrillCollarLength = d.Length.ItemValue
                    }).ToList(),

                DrillingShallowHeight = this.DrillingShallowHeight.ItemValue,
                DrillingDeepHeight = this.DrillingDeepHeight.ItemValue,
                DrillingType = this.DrillingType.ItemValue,
                DrillingDifficulty = this.DrillingDifficulty.ItemValue,

                TripShallowHeight = this.TripShallowHeight.ItemValue,
                TripDeepHeight = this.TripDeepHeight.ItemValue,
                TripCount = this.TripCount.ItemValue,

                BushingWeight = this.BushingWeight.ItemValue,
                BushingLength = this.BushingLength.ItemValue,
                BushingHeight = this.BushingHeight.ItemValue,

                CoringShallowHeight = this.CoringShallowHeight.ItemValue,
                CoringDeepHeight = this.CoringDeepHeight.ItemValue,
                WorkValue = this.TotalWorkValue.ItemValue,

                ConfigTime = configInfo.LastWriteTime
            };
            WorkOperator.UpdateWork(dbContext, Sign.Data.UserID, workData);
        }

        public void Reset()
        {
            SteelWireContext dbContext = new SteelWireContext();
            if (ResetOperator.GetCurrentData(dbContext, Sign.Data.UserID) == null)
            {
                throw new ErrorException("CumulationDataNotFound");
            }
            ResetOperator.Reset(dbContext, Sign.Data.UserID);
        }
    }
}
