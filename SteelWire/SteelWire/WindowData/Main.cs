using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Transactions;
using System.Windows;
using BaseConfig;
using SteelWire.AppCode.Config;
using SteelWire.AppCode.CustomException;
using SteelWire.AppCode.CustomMessage;
using SteelWire.AppCode.Dependencies;
using SteelWire.Business.CalculateCommander;
using SteelWire.Business.Database;
using SteelWire.Business.DbOperator;
using SteelWire.Windows;
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
    /// <summary>
    /// MainWindow的绑定数据
    /// </summary>
    public class Main
    {
        /// <summary>
        /// 单例
        /// </summary>
        public static readonly Main Data;

        static Main()
        {
            Data = new Main
            {
                DerrickHeight = new DependencyItem<decimal>(),
                WirelineMaxPower = new DependencyItem<decimal>(),
                RotaryHookWorkload = new DependencyItem<decimal>(),
                RopeCount = new DependencyItem<int>(),
                RollerDiameter = new DependencyItem<decimal>(),
                WirelineDiameter = new DependencyItem<decimal>(),
                WirelineCutLengthValue = new DependencyItem<decimal>(),
                SecurityCoefficientValue = new DependencyItem<decimal>(),
                PulleyCoefficientValue = new DependencyItem<decimal>(),
                WirelineCuttingCriticalValue = new DependencyItem<decimal>(),

                FluidDensity = new DependencyItem<decimal>(),
                ElevatorWeight = new DependencyItem<decimal>(),
                DrillPipes = new DependencyDrillPipeCollection(),
                DrillCollars = new DependencyDrillCollection<DrillCollarConfig>(),

                DrillingShallowHeight = new DependencyItem<decimal>(),
                DrillingDeepHeight = new DependencyItem<decimal>(),
                DrillingType = new DependencyItem<string>(),
                DrillingDifficulty = new DependencyItem<string>(),
                DrillingWorkValue = new DependencyItem<decimal>(),

                TripShallowHeight = new DependencyItem<decimal>(),
                TripDeepHeight = new DependencyItem<decimal>(),
                TripCount = new DependencyItem<decimal>(),
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
        public bool CanCancelExit { get; set; }

        public DependencyItem<decimal> DerrickHeight { get; private set; }
        public DependencyItem<decimal> WirelineMaxPower { get; private set; }
        public DependencyItem<decimal> RotaryHookWorkload { get; private set; }
        public DependencyItem<int> RopeCount { get; private set; }
        public DependencyItem<decimal> RollerDiameter { get; private set; }
        public DependencyItem<decimal> WirelineDiameter { get; private set; }
        public DependencyItem<decimal> WirelineCutLengthValue { get; private set; }
        public DependencyItem<decimal> SecurityCoefficientValue { get; private set; }
        public DependencyItem<decimal> PulleyCoefficientValue { get; private set; }
        public DependencyItem<decimal> WirelineCuttingCriticalValue { get; private set; }

        public DependencyItem<decimal> FluidDensity { get; private set; }
        public DependencyItem<decimal> ElevatorWeight { get; private set; }
        public DependencyDrillPipeCollection DrillPipes { get; private set; }
        public DependencyDrillCollection<DrillCollarConfig> DrillCollars { get; private set; }

        public DependencyItem<decimal> DrillingShallowHeight { get; private set; }
        public DependencyItem<decimal> DrillingDeepHeight { get; private set; }
        public DependencyItem<string> DrillingType { get; private set; }
        public DependencyItem<string> DrillingDifficulty { get; private set; }
        public DependencyItem<decimal> DrillingWorkValue { get; private set; }

        public DependencyItem<decimal> TripShallowHeight { get; private set; }
        public DependencyItem<decimal> TripDeepHeight { get; private set; }
        public DependencyItem<decimal> TripCount { get; private set; }
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

        /// <summary>
        /// 初始化数据驱动事件
        /// </summary>
        private void InitializeEvent()
        {
            this.DerrickHeight.ItemValueChangedHandler += DerrickHeightChanged;
            this.WirelineMaxPower.ItemValueChangedHandler += SecurityCoefficientValueChanged;
            this.RotaryHookWorkload.ItemValueChangedHandler += SecurityCoefficientValueChanged;
            this.RopeCount.ItemValueChangedHandler += SecurityCoefficientValueChanged;
            this.RollerDiameter.ItemValueChangedHandler += PulleyCoefficientValueChanged;
            this.WirelineDiameter.ItemValueChangedHandler += PulleyCoefficientValueChanged;

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

        /// <summary>
        /// 初始化数据
        /// </summary>
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
            this.DrillPipes.DrillPipes.Clear();
            this.DrillPipes.AddRange(workConf.DrillPipes.Cast<DrillPipe>()
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
                    },
                    StandLength = new DependencyItem<decimal>
                    {
                        ItemValue = d.StandLength
                    }
                }));
            this.DrillPipes.DrillPipes.ResetLocalItems();
            this.DrillPipes.HeavierDrillPipes.Clear();
            this.DrillPipes.AddRange(workConf.HeavierDrillPipes.Cast<Drill>()
                .Select(d => new HeavierDrillPipeConfig
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
            this.DrillPipes.HeavierDrillPipes.ResetLocalItems();
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

            CalculateWirelineCutLength();
            CalculateSecurityCoefficient();
            CalculatePulleyCoefficient();
            CalculateWirelineCuttingCritical();
            CommanderTripOnce commanderOnce = CreateCommanderOnce();
            CalculateDrillingWork(commanderOnce);
            CalculateTripWork(commanderOnce);
            CalculateBushingWork();
            CalculateCoringWork(commanderOnce);
            CalculateTotalWork();
            this._isInitializeData = false;
        }

        /// <summary>
        /// 井架高度变化事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DerrickHeightChanged(object sender, EventArgs e)
        {
            if (!this._isInitializeData)
            {
                CalculateWirelineCutLength();
            }
            WirelineCuttingCriticalValueChanged(sender, e);
        }

        /// <summary>
        /// 安全系数变化事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SecurityCoefficientValueChanged(object sender, EventArgs e)
        {
            if (!this._isInitializeData)
            {
                CalculateSecurityCoefficient();
            }
            WirelineCuttingCriticalValueChanged(sender, e);
        }

        /// <summary>
        /// 钢丝绳直径变化事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PulleyCoefficientValueChanged(object sender, EventArgs e)
        {
            if (!this._isInitializeData)
            {
                CalculatePulleyCoefficient();
            }
            WirelineCuttingCriticalValueChanged(sender, e);
        }

        /// <summary>
        /// 切绳临界值变化事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WirelineCuttingCriticalValueChanged(object sender, EventArgs e)
        {
            if (!this._isInitializeData)
            {
                CalculateWirelineCuttingCritical();
            }
        }

        /// <summary>
        /// 公用系数变化事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// 钻井配置变化事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DrillingWorkValueChanged(object sender, EventArgs e)
        {
            if (!this._isInitializeData)
            {
                CommanderTripOnce commanderOnce = CreateCommanderOnce();
                CalculateDrillingWork(commanderOnce);
                CalculateTotalWork();
            }
        }

        /// <summary>
        /// 起下钻配置变化事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TripWorkValueChanged(object sender, EventArgs e)
        {
            if (!this._isInitializeData)
            {
                CommanderTripOnce commanderOnce = CreateCommanderOnce();
                CalculateTripWork(commanderOnce);
                CalculateTotalWork();
            }
        }

        /// <summary>
        /// 下套管配置变化事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BushingWorkValueChanged(object sender, EventArgs e)
        {
            if (!this._isInitializeData)
            {
                CalculateBushingWork();
                CalculateTotalWork();
            }
        }

        /// <summary>
        /// 取岩心配置变化事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CoringWorkValueChanged(object sender, EventArgs e)
        {
            if (!this._isInitializeData)
            {
                CommanderTripOnce commanderOnce = CreateCommanderOnce();
                CalculateCoringWork(commanderOnce);
                CalculateTotalWork();
            }
        }

        /// <summary>
        /// 构建一次起下钻钻井计算对象
        /// </summary>
        /// <returns></returns>
        private CommanderTripOnce CreateCommanderOnce()
        {
            return new CommanderTripOnce
            {
                FluidDensity = this.FluidDensity.ItemValue,
                ElevatorWeight = this.ElevatorWeight.ItemValue,
                DrillPipeWeight = this.DrillPipes.AverageWeight.ItemValue,
                DrillPipeLength = this.DrillPipes.TotalLength.ItemValue,
                DrillCollarWeight = this.DrillCollars.AverageWeight.ItemValue,
                DrillCollarLength = this.DrillCollars.TotalLength.ItemValue
            };
        }

        /// <summary>
        /// 计算切绳长度
        /// </summary>
        private void CalculateWirelineCutLength()
        {
            CuttingCriticalDictionary criticalDic = CuttingCriticalDictionaryManager.OnceInstance.DictionarySection;
            WireropeCutRole wireropeCutRole = criticalDic.WireropeCutRoles.Cast<WireropeCutRole>()
                .FirstOrDefault(r => r.MinDerrickHeight <= this.DerrickHeight.ItemValue && r.MaxDerrickHeight >= this.DerrickHeight.ItemValue);
            if (wireropeCutRole == null)
            {
                this.WirelineCutLengthValue.ItemValue = -1;
                return;
            }
            CommanderRopeCut commander = new CommanderRopeCut
            {
                WirelineCutMinLength = wireropeCutRole.MinCutLength,
                WirelineCutMaxLength = wireropeCutRole.MaxCutLength
            };
            try
            {
                this.WirelineCutLengthValue.ItemValue = commander.GetWirelineCutLength();
            }
            catch (Exception)
            {
                this.WirelineCutLengthValue.ItemValue = -1;
            }
        }

        /// <summary>
        /// 计算安全系数
        /// </summary>
        private void CalculateSecurityCoefficient()
        {
            CuttingCriticalDictionary criticalDic = CuttingCriticalDictionaryManager.OnceInstance.DictionarySection;
            WireropeEfficiency wireropeEfficiency = criticalDic.WireropeEfficiencies[this.RopeCount.ItemValue];
            if (wireropeEfficiency == null)
            {
                this.SecurityCoefficientValue.ItemValue = -1;
                return;
            }
            CommanderRopeCut commander = new CommanderRopeCut
            {
                WirelineMaxPower = this.WirelineMaxPower.ItemValue,
                RotaryHookWorkload = this.RotaryHookWorkload.ItemValue,
                RopeEfficiency = wireropeEfficiency.RollingValue,
                RopeCount = this.RopeCount.ItemValue
            };
            try
            {
                this.SecurityCoefficientValue.ItemValue = commander.GetSecurityCoefficient();
            }
            catch (Exception)
            {
                this.SecurityCoefficientValue.ItemValue = -1;
            }
        }

        /// <summary>
        /// 计算滑轮系数
        /// </summary>
        private void CalculatePulleyCoefficient()
        {
            CommanderRopeCut commander = new CommanderRopeCut
            {
                RollerDiameter = this.RollerDiameter.ItemValue,
                WirelineDiameter = this.WirelineDiameter.ItemValue
            };
            try
            {
                this.PulleyCoefficientValue.ItemValue = commander.GetPulleyCoefficient();
            }
            catch (Exception)
            {
                this.PulleyCoefficientValue.ItemValue = -1;
            }
        }

        /// <summary>
        /// 计算切绳临界值
        /// </summary>
        private void CalculateWirelineCuttingCritical()
        {
            CuttingCriticalDictionary criticalDic = CuttingCriticalDictionaryManager.OnceInstance.DictionarySection;
            WireropeCutRole wireropeCutRole = criticalDic.WireropeCutRoles.Cast<WireropeCutRole>()
                .FirstOrDefault(r => r.MinDerrickHeight <= this.DerrickHeight.ItemValue && r.MaxDerrickHeight >= this.DerrickHeight.ItemValue);
            if (wireropeCutRole == null)
            {
                this.WirelineCuttingCriticalValue.ItemValue = -1;
                return;
            }
            WireropeWorkload wireropeWorkload = criticalDic.WireropeWorkloads[this.WirelineDiameter.ItemValue];
            if (wireropeWorkload == null)
            {
                this.WirelineCuttingCriticalValue.ItemValue = -1;
                return;
            }
            WireropeEfficiency wireropeEfficiency = criticalDic.WireropeEfficiencies[this.RopeCount.ItemValue];
            if (wireropeEfficiency == null)
            {
                this.WirelineCuttingCriticalValue.ItemValue = -1;
                return;
            }
            CommanderRopeCut commander = new CommanderRopeCut
            {
                WirelineWorkloadPerMetre = wireropeWorkload.Workload,
                WirelineCutMinLength = wireropeCutRole.MinCutLength,
                WirelineCutMaxLength = wireropeCutRole.MaxCutLength,
                WirelineMaxPower = this.WirelineMaxPower.ItemValue,
                RotaryHookWorkload = this.RotaryHookWorkload.ItemValue,
                RopeEfficiency = wireropeEfficiency.RollingValue,
                RopeCount = this.RopeCount.ItemValue,
                RollerDiameter = this.RollerDiameter.ItemValue,
                WirelineDiameter = this.WirelineDiameter.ItemValue
            };
            try
            {
                this.WirelineCuttingCriticalValue.ItemValue = commander.CalculateValue();
            }
            catch (Exception)
            {
                this.WirelineCuttingCriticalValue.ItemValue = -1;
            }
        }

        /// <summary>
        /// 计算钻井作业
        /// </summary>
        /// <param name="commanderOnce"></param>
        private void CalculateDrillingWork(CommanderTripOnce commanderOnce)
        {
            WorkDictionary workDic = WorkDictionaryManager.OnceInstance.DictionarySection;
            DrillingType type = workDic.DrillingTypes[this.DrillingType.ItemValue];
            if (type == null)
            {
                this.DrillingWorkValue.ItemValue = -1;
                return;
            }
            DrillingDifficulty difficulty = workDic.DrillingDifficulties[this.DrillingDifficulty.ItemValue];
            if (difficulty == null)
            {
                this.DrillingWorkValue.ItemValue = -1;
                return;
            }
            CommanderDrilling commander = new CommanderDrilling(commanderOnce)
            {
                Type = type.Coefficient,
                Difficulty = difficulty.Difficulty,
                DeepHeight = this.DrillingDeepHeight.ItemValue,
                ShallowHeight = this.DrillingShallowHeight.ItemValue
            };
            try
            {
                this.DrillingWorkValue.ItemValue = commander.CalculateValue();
            }
            catch (Exception)
            {
                this.DrillingWorkValue.ItemValue = -1;
            }
        }

        /// <summary>
        /// 计算起下钻作业
        /// </summary>
        /// <param name="commanderOnce"></param>
        private void CalculateTripWork(CommanderTripOnce commanderOnce)
        {
            CommanderTrip commander = new CommanderTrip(commanderOnce)
            {
                DeepHeight = this.TripDeepHeight.ItemValue,
                ShallowHeight = this.TripShallowHeight.ItemValue,
                Count = this.TripCount.ItemValue
            };
            try
            {
                this.TripWorkValue.ItemValue = commander.CalculateValue();
            }
            catch (Exception)
            {
                this.TripWorkValue.ItemValue = -1;
            }
        }

        /// <summary>
        /// 计算下套管作业
        /// </summary>
        private void CalculateBushingWork()
        {
            CommanderBushing commander = new CommanderBushing
            {
                FluidDensity = this.FluidDensity.ItemValue,
                ElevatorWeight = this.ElevatorWeight.ItemValue,
                BushingWeight = this.BushingWeight.ItemValue,
                BushingLength = this.BushingLength.ItemValue,
                BushingHeight = this.BushingHeight.ItemValue
            };
            try
            {
                this.BushingWorkValue.ItemValue = commander.CalculateValue();
            }
            catch (Exception)
            {
                this.BushingWorkValue.ItemValue = -1;
            }
        }

        /// <summary>
        /// 计算取岩心作业
        /// </summary>
        /// <param name="commanderOnce"></param>
        private void CalculateCoringWork(CommanderTripOnce commanderOnce)
        {
            CommanderCoring commander = new CommanderCoring(commanderOnce)
            {
                DeepHeight = this.CoringDeepHeight.ItemValue,
                ShallowHeight = this.CoringShallowHeight.ItemValue
            };
            try
            {
                this.CoringWorkValue.ItemValue = commander.CalculateValue();
            }
            catch (Exception)
            {
                this.CoringWorkValue.ItemValue = -1;
            }
        }

        /// <summary>
        /// 计算作业总和
        /// </summary>
        private void CalculateTotalWork()
        {
            if (this.DrillingWorkValue.ItemValue < 0
                || this.TripWorkValue.ItemValue < 0
                || this.BushingWorkValue.ItemValue < 0
                || this.CoringWorkValue.ItemValue < 0)
            {
                this.TotalWorkValue.ItemValue = -1;
                return;
            }
            this.TotalWorkValue.ItemValue = this.DrillingWorkValue.ItemValue
                + this.TripWorkValue.ItemValue
                + this.BushingWorkValue.ItemValue
                + this.CoringWorkValue.ItemValue;
        }

        /// <summary>
        /// 显示登陆页面
        /// </summary>
        /// <returns></returns>
        public bool ShowSignWindow()
        {
            SignWindow signWindow = new SignWindow();
            if (signWindow.ShowDialog() == true)
            {
                CuttingCriticalDictionaryManager.InitializeConfig();
                CuttingCriticalConfigManager.InitializeConfig();
                WorkDictionaryManager.InitializeConfig();
                WorkConfigManager.InitializeConfig();
                this.InitializeData();
                this.RefreshData();
                return true;
            }
            return false;
        }

        /// <summary>
        /// 刷新数据
        /// </summary>
        public void RefreshData()
        {
            this.CanCancelExit = true;
            CumulationReset data = ResetOperator.GetCurrentData(Sign.Data.UserID);
            RefreshSetData(data);
        }

        /// <summary>
        /// 刷新数据
        /// </summary>
        /// <param name="dbContext"></param>
        public void RefreshData(SteelWireContext dbContext)
        {
            CumulationReset data = ResetOperator.GetCurrentData(dbContext, Sign.Data.UserID);
            RefreshSetData(data);
        }

        /// <summary>
        /// 刷新后设置数据
        /// </summary>
        /// <param name="data"></param>
        private void RefreshSetData(CumulationReset data)
        {
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

        /// <summary>
        /// 从数据库下载配置
        /// </summary>
        public void DownLoadConfig()
        {
            // 数据库下载配置信息功能未实现
        }

        /// <summary>
        /// 累计（上传配置和数据到数据库）
        /// </summary>
        /// <returns></returns>
        public bool Cumulate()
        {
            bool done = false;
            DateTime now = DateTime.Now;
            using (SteelWireContext dbContext = new SteelWireContext())
            {
                using (TransactionScope t = new TransactionScope())
                {
                    //if (WorkOperator.ExistWork(dbContext, Sign.Data.UserID, DateTime.Now))
                    //{
                    //    throw new InfoException("HaveCumulatedToday");
                    //}
                    CumulationReset data = ResetOperator.GetCurrentData(dbContext, Sign.Data.UserID);
                    if (data == null)
                    {
                        if (MessageManager.Question("SaveCriticalCumulateCompulsivelyConfirm"))
                        {
                            Cumulate(dbContext, now, true);
                            done = true;
                        }
                    }
                    else
                    {
                        bool? result = MessageManager.Choose("SaveCriticalCumulateConfirm");
                        if (result.HasValue)
                        {
                            if (result.Value)
                            {
                                Cumulate(dbContext, now, true);
                                done = true;
                            }
                            else
                            {
                                Cumulate(dbContext, now);
                                done = true;
                            }
                        }
                    }
                    t.Complete();
                }
            }
            return done;
        }

        /// <summary>
        /// 累计（上传配置和数据到数据库）
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="now"></param>
        /// <param name="reset"></param>
        private void Cumulate(SteelWireContext dbContext, DateTime now, bool reset = false)
        {
            if (reset)
            {
                this.UpdateCritical(dbContext, now);
                dbContext.SaveChanges();
            }
            UpdateWork(dbContext, now);
            dbContext.SaveChanges();
            RefreshData(dbContext);
        }

        /// <summary>
        /// 保存到本地配置文件
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// 上传临界值
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="now"></param>
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

        /// <summary>
        /// 保存到本地配置文件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="drills"></param>
        /// <param name="configs"></param>
        /// <returns></returns>
        private bool ChangeDrillList<T>(AddCollection<DrillPipe> drills, IEnumerable<T> configs)
            where T : DrillPipeConfig, new()
        {
            bool changed = false;
            IEnumerable<DrillPipe> remove = drills.Cast<DrillPipe>().Where(d => configs.All(p => p.Name.ItemValue != d.Name));
            if (remove.Any())
            {
                foreach (DrillPipe drill in remove)
                {
                    drills.Remove(drill);
                }
                changed = true;
            }
            foreach (T config in configs)
            {
                DrillPipe item = drills[config.Name.ItemValue];
                if (item == null)
                {
                    item = new DrillPipe
                    {
                        Name = config.Name.ItemValue,
                        Weight = config.Weight.ItemValue,
                        Length = config.Length.ItemValue,
                        StandLength = config.StandLength.ItemValue
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
                    if (item.StandLength != config.StandLength.ItemValue)
                    {
                        item.StandLength = config.StandLength.ItemValue;
                        changed = true;
                    }
                }
            }
            return changed;
        }

        /// <summary>
        /// 保存到本地配置文件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="drills"></param>
        /// <param name="configs"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 保存到本地配置文件
        /// </summary>
        /// <returns></returns>
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
            if (ChangeDrillList(workConf.DrillPipes, DrillPipes.DrillPipes.Items))
            {
                changed = true;
            }
            if (ChangeDrillList(workConf.HeavierDrillPipes, DrillPipes.HeavierDrillPipes.Items))
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

        /// <summary>
        /// 上传作业累计值
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="now"></param>
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
                DrillPipeConfig = this.DrillPipes.DrillPipes.Items
                    .Where(d => d.Weight.ItemValue > 0 && d.Length.ItemValue > 0 && d.StandLength.ItemValue > 0)
                    .Select(d => new DbDrillPipeConfig
                    {
                        DrillPipeName = d.Name.ItemValue,
                        DrillPipeWeight = d.Weight.ItemValue,
                        DrillPipeLength = d.Length.ItemValue,
                        DrillPipeType = DrillPipeTypeEnum.DrillPipe.ToString(),
                        DrillPipeStandLength = d.StandLength.ItemValue
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
            IEnumerable<DbDrillPipeConfig> heavierDrillPipes = this.DrillPipes.HeavierDrillPipes.Items
                .Where(d => d.Weight.ItemValue > 0 && d.Length.ItemValue > 0)
                .Select(d => new DbDrillPipeConfig
                {
                    DrillPipeName = d.Name.ItemValue,
                    DrillPipeWeight = d.Weight.ItemValue,
                    DrillPipeLength = d.Length.ItemValue,
                    DrillPipeType = DrillPipeTypeEnum.HeavierDrillPipe.ToString(),
                    DrillPipeStandLength = 0
                });
            foreach (DbDrillPipeConfig heavierDrillPipe in heavierDrillPipes)
            {
                workData.DrillPipeConfig.Add(heavierDrillPipe);
            }
            WorkOperator.UpdateWork(dbContext, Sign.Data.UserID, workData, Math.Round(this.WirelineCuttingCriticalValue.ItemValue, 8));
        }

        /// <summary>
        /// 判断是否需要切绳
        /// </summary>
        /// <returns></returns>
        public bool CheckNeedReset()
        {
            return this.CriticalValue.ItemValue > 0 && this.CumulationValue.ItemValue >= this.CriticalValue.ItemValue;
        }

        /// <summary>
        /// 切绳并上传数据
        /// </summary>
        /// <param name="warningMode"></param>
        /// <returns></returns>
        public void Reset(bool warningMode)
        {
            SteelWireContext dbContext = new SteelWireContext();
            //if (ResetOperator.ExistReset(dbContext, Sign.Data.UserID, DateTime.Now))
            //{
            //    throw new InfoException("HaveResetToday");
            //}
            CumulationReset data = ResetOperator.GetCurrentData(dbContext, Sign.Data.UserID);
            if (data == null)
            {
                throw new ErrorException("CumulationDataNotFound");
            }
            if (warningMode)
            {
                bool? choose = MessageManager.WarningChoose("ResetCompulsivelyConfirm");
                if (choose == null)
                {
                    this.CanCancelExit = false;
                    Application.Current.Shutdown();
                    return;
                }
                if (choose.Value)
                {
                    Reset(dbContext, data);
                    return;
                }
                ShowSignWindow();
                if (CheckNeedReset())
                {
                    Reset(true);
                }
            }
            else if (MessageManager.Question("ResetConfirm"))
            {
                Reset(dbContext, data);
            }
        }

        /// <summary>
        /// 切绳并上传数据
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="data"></param>
        private void Reset(SteelWireContext dbContext, CumulationReset data)
        {
            ResetOperator.Reset(dbContext, Sign.Data.UserID, data);
            dbContext.SaveChanges();
            RefreshData(dbContext);
        }
    }
}
