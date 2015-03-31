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
        public static readonly Main Data;

        static Main()
        {
            Data = new Main
            {
                DerrickHeight = new DependencyItem<decimal>(),
                WirelineMaxPower = new DependencyItem<decimal>(),
                RotaryHookWorkload = new DependencyItem<decimal>(),
                RollerDiameter = new DependencyItem<decimal>(),
                WirelineDiameter = new DependencyItem<decimal>(),
                RopeCount = new DependencyItem<int>(),
                WirelineCuttingCriticalValue = new DependencyItem<decimal>(),

                FluidDensity = new DependencyItem<decimal>(),
                ElevatorWeight = new DependencyItem<decimal>(),
                DrillPipes = new DependencyDrillCollection<DrillPipeConfig>(),
                DrillCollars = new DependencyDrillCollection<DrillCollarConfig>(),

                DrillingShallowHeight = new DependencyItem<decimal>(),
                DrillingDeepHeight = new DependencyItem<decimal>(),
                DrillingType = new DependencyItem<string>(),
                DrillingDifficulty = new DependencyItem<string>(),
                DrillingWorkValue = new DependencyItem<decimal>(),

                TripShallowHeight = new DependencyItem<decimal>(),
                TripDeepHeight = new DependencyItem<decimal>(),
                TripCount = new DependencyItem<int>(),
                TripWorkValue = new DependencyItem<decimal>(),

                BushingWeight = new DependencyItem<decimal>(),
                BushingLength = new DependencyItem<decimal>(),
                BushingHeight = new DependencyItem<decimal>(),
                BushingWorkValue = new DependencyItem<decimal>(),

                CoringShallowHeight = new DependencyItem<decimal>(),
                CoringDeepHeight = new DependencyItem<decimal>(),
                CoringWorkValue = new DependencyItem<decimal>(),

                TotalWorkValue = new DependencyItem<decimal>(),

                UserDisplay = Sign.Data.UserDisplay,
                CriticalValue = new DependencyItem<decimal>(),
                CumulationValue = new DependencyItem<decimal>(),
                Log = new DependencyItem<string>()
            };

            Data.InitializeData();
            Data.InitializeEvent();
        }

        private bool _isInitializeData;

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

        private Main()
        {

        }

        private void InitializeEvent()
        {
            //OnceInstance.DerrickHeight.ItemValueChangedHandler += WirelineCuttingCriticalValueChanged;
            this.WirelineMaxPower.ItemValueChangedHandler += WirelineCuttingCriticalValueChanged;
            this.RotaryHookWorkload.ItemValueChangedHandler += WirelineCuttingCriticalValueChanged;
            this.RollerDiameter.ItemValueChangedHandler += WirelineCuttingCriticalValueChanged;
            this.WirelineDiameter.ItemValueChangedHandler += WirelineCuttingCriticalValueChanged;
            this.RopeCount.ItemValueChangedHandler += WirelineCuttingCriticalValueChanged;

            this.FluidDensity.ItemValueChangedHandler += CommonCoefficientValueChanged;
            this.ElevatorWeight.ItemValueChangedHandler += CommonCoefficientValueChanged;
            this.DrillPipes.ItemsChangedHandler += CommonCoefficientValueChanged;
            this.DrillCollars.ItemsChangedHandler += CommonCoefficientValueChanged;

            this.DrillingShallowHeight.ItemValueChangedHandler += DrillingWorkValueChanged;
            this.DrillingDeepHeight.ItemValueChangedHandler += DrillingWorkValueChanged;
            this.DrillingType.ItemValueChangedHandler += DrillingWorkValueChanged;
            this.DrillingDifficulty.ItemValueChangedHandler += DrillingWorkValueChanged;

            this.TripDeepHeight.ItemValueChangedHandler += TripWorkValueChanged;
            this.TripShallowHeight.ItemValueChangedHandler += TripWorkValueChanged;
            this.TripCount.ItemValueChangedHandler += TripWorkValueChanged;

            this.BushingWeight.ItemValueChangedHandler += BushingWorkValueChanged;
            this.BushingLength.ItemValueChangedHandler += BushingWorkValueChanged;
            this.BushingHeight.ItemValueChangedHandler += BushingWorkValueChanged;

            this.CoringShallowHeight.ItemValueChangedHandler += CoringWorkValueChanged;
            this.CoringDeepHeight.ItemValueChangedHandler += CoringWorkValueChanged;
        }

        public void InitializeData()
        {
            this._isInitializeData = true;
            CuttingCriticalConfig criticalConf = CuttingCriticalConfigManager.OnceInstance.ConfigSection;
            WorkConfig workConf = WorkConfigManager.OnceInstance.ConfigSection;

            this.DerrickHeight.ItemValue = criticalConf.DerrickHeight;
            this.WirelineMaxPower.ItemValue = criticalConf.WirelineMaxPower;
            this.RotaryHookWorkload.ItemValue = criticalConf.RotaryHookWorkload;
            this.RollerDiameter.ItemValue = criticalConf.RollerDiameter;
            this.WirelineDiameter.ItemValue = criticalConf.WirelineDiameter;
            this.RopeCount.ItemValue = criticalConf.RopeCount;

            this.FluidDensity.ItemValue = workConf.FluidDensity;
            this.ElevatorWeight.ItemValue = workConf.ElevatorWeight;
            this.DrillPipes.Clear();
            this.DrillPipes.AddRange(workConf.DrillPipes.Cast<Drill>()
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
            this.DrillPipes.ResetLocalItems();
            this.DrillCollars.Clear();
            this.DrillCollars.AddRange(workConf.DrillCollars.Cast<Drill>()
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
            this.DrillCollars.ResetLocalItems();

            this.DrillingShallowHeight.ItemValue = workConf.DrillingShallowHeight;
            this.DrillingDeepHeight.ItemValue = workConf.DrillingDeepHeight;
            this.DrillingType.ItemValue = workConf.DrillingType;
            this.DrillingDifficulty.ItemValue = workConf.DrillingDifficulty;

            this.TripShallowHeight.ItemValue = workConf.TripShallowHeight;
            this.TripDeepHeight.ItemValue = workConf.TripDeepHeight;
            this.TripCount.ItemValue = workConf.TripCount;

            this.BushingWeight.ItemValue = workConf.BushingWeight;
            this.BushingLength.ItemValue = workConf.BushingLength;
            this.BushingHeight.ItemValue = workConf.BushingHeight;

            this.CoringShallowHeight.ItemValue = workConf.CoringShallowHeight;
            this.CoringDeepHeight.ItemValue = workConf.CoringDeepHeight;

            this.UserDisplay = Sign.Data.UserDisplay;

            CalculateWirelineCuttingCritical();
            CommanderTripOnce commanderOnce = CreateCommanderOnce();
            CalculateDrillingWork(commanderOnce);
            CalculateTripWork(commanderOnce);
            CalculateBushingWork();
            CalculateCoringWork(commanderOnce);
            CalculateTotalWork();
            this._isInitializeData = false;
        }

        private void WirelineCuttingCriticalValueChanged(object sender, EventArgs e)
        {
            if (!this._isInitializeData)
            {
                CalculateWirelineCuttingCritical();
            }
        }

        private void CommonCoefficientValueChanged(object sender, EventArgs e)
        {
            if (!this._isInitializeData)
            {
                CommanderTripOnce commanderOnce = CreateCommanderOnce();
                CalculateDrillingWork(commanderOnce);
                CalculateTripWork(commanderOnce);
                CalculateBushingWork();
                CalculateCoringWork(commanderOnce);
                CalculateTotalWork();
            }
        }

        private void DrillingWorkValueChanged(object sender, EventArgs e)
        {
            if (!this._isInitializeData)
            {
                CommanderTripOnce commanderOnce = CreateCommanderOnce();
                CalculateDrillingWork(commanderOnce);
                CalculateTotalWork();
            }
        }

        private void TripWorkValueChanged(object sender, EventArgs e)
        {
            if (!this._isInitializeData)
            {
                CommanderTripOnce commanderOnce = CreateCommanderOnce();
                CalculateTripWork(commanderOnce);
                CalculateTotalWork();
            }
        }

        private void BushingWorkValueChanged(object sender, EventArgs e)
        {
            if (!this._isInitializeData)
            {
                CalculateBushingWork();
                CalculateTotalWork();
            }
        }

        private void CoringWorkValueChanged(object sender, EventArgs e)
        {
            if (!this._isInitializeData)
            {
                CommanderTripOnce commanderOnce = CreateCommanderOnce();
                CalculateCoringWork(commanderOnce);
                CalculateTotalWork();
            }
        }

        private CommanderTripOnce CreateCommanderOnce()
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

        private void CalculateWirelineCuttingCritical()
        {
            CuttingCriticalDictionary criticalDic = CuttingCriticalDictionaryManager.OnceInstance.DictionarySection;
            WireropeWorkload wireropeWorkload = criticalDic.WireropeWorkloads[Data.WirelineDiameter.ItemValue];
            if (wireropeWorkload == null)
            {
                Data.WirelineCuttingCriticalValue.ItemValue = -1;
                return;
            }
            WireropeEfficiency wireropeEfficiency = criticalDic.WireropeEfficiencies[Data.RopeCount.ItemValue];
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

        private void CalculateDrillingWork(CommanderTripOnce commanderOnce)
        {
            WorkDictionary workDic = WorkDictionaryManager.OnceInstance.DictionarySection;
            DrillingType type = workDic.DrillingTypes[Data.DrillingType.ItemValue];
            if (type == null)
            {
                Data.DrillingWorkValue.ItemValue = -1;
                return;
            }
            DrillingDifficulty difficulty = workDic.DrillingDifficulties[Data.DrillingDifficulty.ItemValue];
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

        private void CalculateTripWork(CommanderTripOnce commanderOnce)
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

        private void CalculateBushingWork()
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

        private void CalculateCoringWork(CommanderTripOnce commanderOnce)
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

        private void CalculateTotalWork()
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

        public void RefreshData()
        {
            CumulationReset data = ResetOperator.GetCurrentData(Sign.Data.UserID);
            if (data != null)
            {
                this.CriticalValue.ItemValue = data.CriticalValue;
                this.CumulationValue.ItemValue = data.CumulationValue;
            }
            else
            {
                this.CriticalValue.ItemValue = 0;
                this.CumulationValue.ItemValue = 0;
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

        private bool ChangeToCriticalConfig()
        {
            CuttingCriticalConfigManager.OnceInstance.ReloadConfig();
            CuttingCriticalConfig criticalConf = CuttingCriticalConfigManager.OnceInstance.ConfigSection;
            bool changed = false;
            if (criticalConf.DerrickHeight != DerrickHeight.ItemValue)
            {
                criticalConf.DerrickHeight = DerrickHeight.ItemValue;
                changed = true;
            }
            if (criticalConf.WirelineMaxPower != WirelineMaxPower.ItemValue)
            {
                criticalConf.WirelineMaxPower = WirelineMaxPower.ItemValue;
                changed = true;
            }
            if (criticalConf.RotaryHookWorkload != RotaryHookWorkload.ItemValue)
            {
                criticalConf.RotaryHookWorkload = RotaryHookWorkload.ItemValue;
                changed = true;
            }
            if (criticalConf.RollerDiameter != RollerDiameter.ItemValue)
            {
                criticalConf.RollerDiameter = RollerDiameter.ItemValue;
                changed = true;
            }
            if (criticalConf.WirelineDiameter != WirelineDiameter.ItemValue)
            {
                criticalConf.WirelineDiameter = WirelineDiameter.ItemValue;
                changed = true;
            }
            if (criticalConf.RopeCount != RopeCount.ItemValue)
            {
                criticalConf.RopeCount = RopeCount.ItemValue;
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
                dicInfo.LastWriteTime.Ticks, out overWrite, out dicData);
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
                    ConfigTime = now,
                    ConfigTimeStamp = dicInfo.LastWriteTime.Ticks,
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
                    configInfo.LastWriteTime.Ticks, criticalValue, out overWrite);
            }
            if (dicUpdate || configChanged)
            {
                configInfo.LastWriteTime = now;
                DbCuttingCriticalConfig configData = new DbCuttingCriticalConfig
                {
                    DictionaryID = dicData.ID,
                    ConfigUserID = Sign.Data.UserID,
                    ConfigTime = now,
                    ConfigTimeStamp = configInfo.LastWriteTime.Ticks,
                    DerrickHeight = this.DerrickHeight.ItemValue,
                    WirelineMaxPower = this.WirelineMaxPower.ItemValue,
                    RotaryHookWorkload = this.RotaryHookWorkload.ItemValue,
                    RollerDiameter = this.RollerDiameter.ItemValue,
                    WirelineDiameter = this.WirelineDiameter.ItemValue,
                    RopeCount = this.RopeCount.ItemValue,
                    CuttingCriticalValue = criticalValue
                };
                CriticalOperator.UpdateCriticalValue(dbContext, Sign.Data.UserID, configData);
            }
        }

        private bool ChangeDrillList<T>(AddCollection<Drill> drills, IEnumerable<T> configs)
            where T : DrillConfig, new()
        {
            bool changed = false;
            IEnumerable<Drill> remove = drills.Cast<Drill>().Where(d => configs.All(p => p.Name.ItemValue != d.Name));
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
            WorkConfig workConf = WorkConfigManager.OnceInstance.ConfigSection;
            bool changed = false;

            #region Common
            if (workConf.FluidDensity != FluidDensity.ItemValue)
            {
                workConf.FluidDensity = FluidDensity.ItemValue;
                changed = true;
            }
            if (workConf.ElevatorWeight != ElevatorWeight.ItemValue)
            {
                workConf.ElevatorWeight = ElevatorWeight.ItemValue;
                changed = true;
            }
            if (ChangeDrillList(workConf.DrillPipes, DrillPipes.Items))
            {
                changed = true;
            }
            if (ChangeDrillList(workConf.DrillCollars, DrillCollars.Items))
            {
                changed = true;
            }
            #endregion

            #region Drilling
            if (workConf.DrillingShallowHeight != DrillingShallowHeight.ItemValue)
            {
                workConf.DrillingShallowHeight = DrillingShallowHeight.ItemValue;
                changed = true;
            }
            if (workConf.DrillingDeepHeight != DrillingDeepHeight.ItemValue)
            {
                workConf.DrillingDeepHeight = DrillingDeepHeight.ItemValue;
                changed = true;
            }
            if (workConf.DrillingType != DrillingType.ItemValue)
            {
                workConf.DrillingType = DrillingType.ItemValue;
                changed = true;
            }
            if (workConf.DrillingDifficulty != DrillingDifficulty.ItemValue)
            {
                workConf.DrillingDifficulty = DrillingDifficulty.ItemValue;
                changed = true;
            }
            #endregion

            #region Trip
            if (workConf.TripShallowHeight != TripShallowHeight.ItemValue)
            {
                workConf.TripShallowHeight = TripShallowHeight.ItemValue;
                changed = true;
            }
            if (workConf.TripDeepHeight != TripDeepHeight.ItemValue)
            {
                workConf.TripDeepHeight = TripDeepHeight.ItemValue;
                changed = true;
            }
            if (workConf.TripCount != TripCount.ItemValue)
            {
                workConf.TripCount = TripCount.ItemValue;
                changed = true;
            }
            #endregion

            #region Bushing
            if (workConf.BushingWeight != BushingWeight.ItemValue)
            {
                workConf.BushingWeight = BushingWeight.ItemValue;
                changed = true;
            }
            if (workConf.BushingLength != BushingLength.ItemValue)
            {
                workConf.BushingLength = BushingLength.ItemValue;
                changed = true;
            }
            if (workConf.BushingHeight != BushingHeight.ItemValue)
            {
                workConf.BushingHeight = BushingHeight.ItemValue;
                changed = true;
            }
            #endregion

            #region Coring
            if (workConf.CoringShallowHeight != CoringShallowHeight.ItemValue)
            {
                workConf.CoringShallowHeight = CoringShallowHeight.ItemValue;
                changed = true;
            }
            if (workConf.CoringDeepHeight != CoringDeepHeight.ItemValue)
            {
                workConf.CoringDeepHeight = CoringDeepHeight.ItemValue;
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
            bool overWrite;
            DbWorkDictionary dicData;
            bool dicUpdate = WorkOperator.IsNeedUpdateDictionary(dbContext, Sign.Data.UserID,
                dicInfo.LastWriteTime.Ticks, out overWrite, out dicData);
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
                    ConfigTime = now,
                    ConfigTimeStamp = dicInfo.LastWriteTime.Ticks,
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
                ConfigTime = now,
                ConfigTimeStamp = configInfo.LastWriteTime.Ticks,
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
                WorkValue = this.TotalWorkValue.ItemValue
            };
            WorkOperator.UpdateWork(dbContext, Sign.Data.UserID, workData, Math.Round(this.WirelineCuttingCriticalValue.ItemValue, 8));
        }

        public void Reset()
        {
            SteelWireContext dbContext = new SteelWireContext();
            CumulationReset data = ResetOperator.GetCurrentData(dbContext, Sign.Data.UserID);
            if (data == null)
            {
                throw new ErrorException("CumulationDataNotFound");
            }
            ResetOperator.Reset(dbContext, Sign.Data.UserID, data);
            dbContext.SaveChanges();
        }
    }
}
