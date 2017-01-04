using BaseConfig;
using SteelWire.AppCode.Config;
using SteelWire.AppCode.CustomException;
using SteelWire.AppCode.CustomMessage;
using SteelWire.AppCode.Dependencies;
using SteelWire.Business.CalculateCommander;
using SteelWire.Business.Database;
using SteelWire.Business.DbOperator;
using SteelWire.Windows;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.IO;
using System.Linq;
using IntelliLock.Licensing;
using SteelWire.AppCode.Data;
using SteelWire.Business.Config;
using SteelWire.Language;
using ConstDictionary = SteelWire.AppCode.Config.ConstDictionary;
using WireropeWorkload = SteelWire.AppCode.Config.WireropeWorkload;
using WireropeCutRole = SteelWire.AppCode.Config.WireropeCutRole;
using WireropeEfficiency = SteelWire.AppCode.Config.WireropeEfficiency;

using DrillingType = SteelWire.AppCode.Config.DrillingType;

using DbWireropeWorkload = SteelWire.Business.Database.WireropeWorkload;
using DbWireropeCutRole = SteelWire.Business.Database.WireropeCutRole;
using DbWireropeEfficiency = SteelWire.Business.Database.WireropeEfficiency;

using DbDrillingType = SteelWire.Business.Database.DrillingType;

namespace SteelWire.WindowData
{
    /// <summary>
    /// MainWindow的绑定数据
    /// </summary>
    public class Main
    {
        private bool _isInitializeData;
        public bool CanCancelExit { get; set; }

        public DependencyLanguage WindowTitle { get; }
        public DependencyLanguage WelcomeText { get; }
        public DependencyLanguage SummaryText { get; }

        public DependencyCollection<WireropeWorkload> WireropeWorkloadCollection { get; } =
            new DependencyCollection<WireropeWorkload>();

        public DependencyObject<decimal> DerrickHeight { get; } = new DependencyObject<decimal>();
        public DependencyObject<decimal> WirelineMaxPower { get; } = new DependencyObject<decimal>();
        public DependencyObject<decimal> RotaryHookWorkload { get; } = new DependencyObject<decimal>();
        public DependencyObject<long> RopeCount { get; } = new DependencyObject<long>();
        public DependencyObject<decimal> RollerDiameter { get; } = new DependencyObject<decimal>();

        public DependencyObject<decimal> RealRotaryHookWorkload { get; } = new DependencyObject<decimal>();
        public DependencyObject<decimal> FluidDensity { get; } = new DependencyObject<decimal>();
        public DependencyObject<decimal> ElevatorWeight { get; } = new DependencyObject<decimal>();

        public DependencyDrillCollection<DependencyDrillConfig> DrillPipes { get; } =
            new DependencyDrillCollection<DependencyDrillConfig>();

        public DependencyDrillCollection<DependencyDrillConfig> HeavierDrillPipes { get; } =
            new DependencyDrillCollection<DependencyDrillConfig>();

        public DependencyDrillCollection<DependencyDrillConfig> DrillCollars { get; } =
            new DependencyDrillCollection<DependencyDrillConfig>();

        public DependencyObject<decimal> DrillingShallowHeight { get; } = new DependencyObject<decimal>();
        public DependencyObject<decimal> DrillingDeepHeight { get; } = new DependencyObject<decimal>();
        public DependencyObject<DrillingTypeEnum> DrillingType { get; } = new DependencyObject<DrillingTypeEnum>();
        public DependencyObject<long> RedressingCount { get; } = new DependencyObject<long>();
        public DependencyObject<decimal> DrillingWorkValue { get; } = new DependencyObject<decimal>();

        public DependencyObject<decimal> TripShallowHeight { get; } = new DependencyObject<decimal>();
        public DependencyObject<decimal> TripDeepHeight { get; } = new DependencyObject<decimal>();
        public DependencyObject<long> TripCount { get; } = new DependencyObject<long>();
        public DependencyObject<decimal> TripWorkValue { get; } = new DependencyObject<decimal>();

        public DependencyDrillCollection<DependencyDrillConfig> Bushings { get; } =
            new DependencyDrillCollection<DependencyDrillConfig>();

        public DependencyObject<decimal> BushingHeight { get; } = new DependencyObject<decimal>();
        public DependencyObject<decimal> BushingWorkValue { get; } = new DependencyObject<decimal>();

        public DependencyObject<decimal> CoringShallowHeight { get; } = new DependencyObject<decimal>();
        public DependencyObject<decimal> CoringDeepHeight { get; } = new DependencyObject<decimal>();
        public DependencyObject<decimal> CoringWorkValue { get; } = new DependencyObject<decimal>();

        public DependencyObject<decimal> CumulationValue { get; } = new DependencyObject<decimal>();

        public DependencyObject<decimal> CriticalValue { get; } = new DependencyObject<decimal>();
        public DependencyObject<decimal> CriticalLength { get; } = new DependencyObject<decimal>();
        public DependencyObject<string> WirelineNumber { get; } = new DependencyObject<string>();
        public DependencyObject<decimal> CutLengthValue { get; } = new DependencyObject<decimal>();
        public DependencyCollection<HistoryData> History { get; } = new DependencyCollection<HistoryData>();

        public Main()
        {
            const string license = "License";
            const string trial = "Trial";
            const string trialRemoveDay = "TrialRemoveDay";

            this.WindowTitle = DependencyLanguage.Generate(() =>
                $"{LanguageManager.GetLocalResourceStringLeft("AppTitle")} {GlobalData.AppVersion} {LanguageManager.GetLocalResourceStringLeft("Welcome")}{GlobalData.UserDisplay.Value} {(CurrentLicense.License.LicenseStatus == LicenseStatus.Licensed ? null : CurrentLicense.License.ExpirationDays_Enabled ? $"[{LanguageManager.GetLocalResourceStringRight(license, trial)} {CurrentLicense.License.ExpirationDays}{LanguageManager.GetLocalResourceStringRight(license, trialRemoveDay)}]" : $"[{LanguageManager.GetLocalResourceStringRight(license, trial)}]")}");
            this.WelcomeText = DependencyLanguage.Generate(
                () => $"{LanguageManager.GetLocalResourceStringLeft("Welcome")}{GlobalData.UserDisplay.Value}");
            this.SummaryText = DependencyLanguage.Generate(() =>
                $"{LanguageManager.GetLocalResourceStringLeft("GroupCumulationAndResetTitle")} -- {LanguageManager.GetLocalResourceStringLeft("LblWirelineCuttingTitle")}{(this.CriticalValue.Value > 0 ? $"{this.CriticalValue.Value:F3}" : "0")}{UnitData.TonKilometre.Value} -- {LanguageManager.GetLocalResourceStringLeft("LblWirelineCutLength")}{(this.CriticalLength.Value > 0 ? $"{this.CriticalLength.Value:F3}" : "0")}{UnitData.Metre.Value}");

            InitializeData();
            InitializeEvent();
        }

        /// <summary>
        /// 初始化数据驱动事件
        /// </summary>
        private void InitializeEvent()
        {
            GlobalData.UserDisplay.ValueChangedHandler += UserDisplayChanged;
            GlobalData.Wireline.Diameter.ValueChangedHandler += CriticalValueChanged;
            GlobalData.Wireline.UnitSystem.ValueChangedHandler += UnitSystemChanged;

            this.DerrickHeight.ValueChangedHandler += DerrickHeightChanged;
            this.WirelineMaxPower.ValueChangedHandler += AllSecurityCoefficientValueChanged;
            this.RotaryHookWorkload.ValueChangedHandler += CriticalValueChanged;
            this.RopeCount.ValueChangedHandler += AllSecurityCoefficientValueChanged;
            this.RollerDiameter.ValueChangedHandler += CriticalValueChanged;

            this.RealRotaryHookWorkload.ValueChangedHandler += CommonCoefficientValueChanged;
            this.FluidDensity.ValueChangedHandler += CommonCoefficientValueChanged;
            this.ElevatorWeight.ValueChangedHandler += CommonCoefficientValueChanged;
            this.DrillPipes.ItemsChangedHandler += CommonCoefficientValueChanged;
            this.HeavierDrillPipes.ItemsChangedHandler += CommonCoefficientValueChanged;
            this.DrillCollars.ItemsChangedHandler += CommonCoefficientValueChanged;

            this.DrillingShallowHeight.ValueChangedHandler += DrillingWorkValueChanged;
            this.DrillingDeepHeight.ValueChangedHandler += DrillingWorkValueChanged;
            this.DrillingType.ValueChangedHandler += DrillingWorkValueChanged;
            this.RedressingCount.ValueChangedHandler += DrillingWorkValueChanged;

            this.TripDeepHeight.ValueChangedHandler += TripWorkValueChanged;
            this.TripShallowHeight.ValueChangedHandler += TripWorkValueChanged;
            this.TripCount.ValueChangedHandler += TripWorkValueChanged;

            this.Bushings.ItemsChangedHandler += BushingWorkValueChanged;
            this.BushingHeight.ValueChangedHandler += BushingWorkValueChanged;

            this.CoringShallowHeight.ValueChangedHandler += CoringWorkValueChanged;
            this.CoringDeepHeight.ValueChangedHandler += CoringWorkValueChanged;

            this.CriticalValue.ValueChangedHandler += SummaryTextChanged;
            this.CriticalLength.ValueChangedHandler += SummaryTextChanged;
            UnitData.TonKilometre.ValueChangedHandler += SummaryTextChanged;
            UnitData.Metre.ValueChangedHandler += SummaryTextChanged;
        }

        private void UserDisplayChanged(object sender, EventArgs e)
        {
            this.WindowTitle.Value = this.WindowTitle.VlueGenerater?.Invoke();
            this.WelcomeText.Value = this.WelcomeText.VlueGenerater?.Invoke();
        }

        private void SummaryTextChanged(object sender, EventArgs e)
        {
            this.SummaryText.Value = this.SummaryText.VlueGenerater?.Invoke();
        }

        private void UnitSystemChanged(object sender, EventArgs e)
        {
            if (!this._isInitializeData)
            {
                InitializeWireropeWorkloads();
                CommonCoefficientValueChanged(sender, e);
            }
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        public void InitializeData()
        {
            this._isInitializeData = true;

            InitializeWireropeWorkloads();

            WireropeWorkload wireropeWorkload = ConstDictionary.WireropeWorkloads.FirstOrDefault(d =>
                d.UnitSystem == GlobalData.Wireline.UnitSystem.Value);
            if (wireropeWorkload != null)
            {
                GlobalData.Wireline.Diameter.Value = wireropeWorkload.Name;
            }
            this.WirelineNumber.Value = UserConfigManager.OnceInstance.LastWirelineNumber;

            CuttingCriticalConfig criticalConf = CuttingCriticalConfigManager.OnceInstance.ConfigSection;
            WorkConfig workConf = WorkConfigManager.OnceInstance.ConfigSection;

            this.DerrickHeight.Value = criticalConf.DerrickHeight;
            this.WirelineMaxPower.Value = criticalConf.WirelineMaxPower;
            this.RotaryHookWorkload.Value = criticalConf.RotaryHookWorkload;
            this.RollerDiameter.Value = criticalConf.RollerDiameter;
            this.RopeCount.Value = criticalConf.RopeCount;

            this.RealRotaryHookWorkload.Value = workConf.RealRotaryHookWorkload;
            this.FluidDensity.Value = workConf.FluidDensity;
            this.ElevatorWeight.Value = workConf.ElevatorWeight;
            this.DrillPipes.Clear();
            this.DrillPipes.AddRange(workConf.DrillPipes.Cast<Drill>()
                .Select(d => new DependencyDrillConfig(DrillDeviceTypeEnum.DrillPipe, d.Name, d.Weight, d.Length)));
            this.HeavierDrillPipes.Clear();
            this.HeavierDrillPipes.AddRange(workConf.HeavierDrillPipes.Cast<Drill>()
                .Select(d => new DependencyDrillConfig(DrillDeviceTypeEnum.HeavierDrillPipe, d.Name, d.Weight, d.Length)));
            this.DrillCollars.Clear();
            this.DrillCollars.AddRange(workConf.DrillCollars.Cast<Drill>()
                .Select(d => new DependencyDrillConfig(DrillDeviceTypeEnum.DrillCollar, d.Name, d.Weight, d.Length)));

            this.DrillingShallowHeight.Value = workConf.DrillingShallowHeight;
            this.DrillingDeepHeight.Value = workConf.DrillingDeepHeight;
            this.DrillingType.Value = workConf.DrillingType;
            this.RedressingCount.Value = workConf.RedressingCount;

            this.TripShallowHeight.Value = workConf.TripShallowHeight;
            this.TripDeepHeight.Value = workConf.TripDeepHeight;
            this.TripCount.Value = workConf.TripCount;

            this.Bushings.Clear();
            this.Bushings.AddRange(workConf.Bushings.Cast<Drill>()
                .Select(d => new DependencyDrillConfig(DrillDeviceTypeEnum.Bushing, d.Name, d.Weight, d.Length)));
            this.BushingHeight.Value = workConf.BushingHeight;

            this.CoringShallowHeight.Value = workConf.CoringShallowHeight;
            this.CoringDeepHeight.Value = workConf.CoringDeepHeight;

            CalculateWirelineCutLength();
            CalculateWirelineCuttingCritical();
            CommanderTripOnce commanderOnce = CreateCommanderOnce();
            CalculateDrillingWork(commanderOnce);
            CalculateTripWork(commanderOnce);
            CalculateBushingWork();
            CalculateCoringWork(commanderOnce);
            CalculateTotalWork();

            this._isInitializeData = false;
        }

        private void InitializeWireropeWorkloads()
        {
            CuttingCriticalDictionary criticalDic = CuttingCriticalDictionaryManager.OnceInstance.DictionarySection;
            this.WireropeWorkloadCollection.Clear();
            this.WireropeWorkloadCollection.AddRange(criticalDic.WireropeWorkloads.Cast<WireropeWorkload>()
                .Where(l => l.UnitSystem == GlobalData.Wireline.UnitSystem.Value));
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
                CalculateWirelineCuttingCritical();
            }
        }

        /// <summary>
        /// 计算切绳长度
        /// </summary>
        private void CalculateWirelineCutLength()
        {
            CuttingCriticalDictionary criticalDic = CuttingCriticalDictionaryManager.OnceInstance.DictionarySection;
            WireropeCutRole wireropeCutRole = criticalDic.WireropeCutRoles.Cast<WireropeCutRole>()
                .FirstOrDefault(
                    r =>
                        r.MinDerrickHeight <= this.DerrickHeight.Value && r.MaxDerrickHeight >= this.DerrickHeight.Value);
            if (wireropeCutRole == null)
            {
                this.CriticalLength.Value = -1;
                return;
            }
            CommanderRopeCut commander = new CommanderRopeCut
            {
                WirelineCutMinLength = wireropeCutRole.MinCutLength,
                WirelineCutMaxLength = wireropeCutRole.MaxCutLength
            };
            try
            {
                this.CriticalLength.Value = commander.GetWirelineCutLength();
            }
            catch (Exception)
            {
                this.CriticalLength.Value = -1;
            }
        }

        /// <summary>
        /// 安全系数变化事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AllSecurityCoefficientValueChanged(object sender, EventArgs e)
        {
            if (!this._isInitializeData)
            {
                CriticalValueChanged(sender, e);
                CommonCoefficientValueChanged(sender, e);
            }
        }

        /// <summary>
        /// 切绳临界值变化事件
        /// </summary>
        private void CriticalValueChanged(object sender, EventArgs e)
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
        /// 计算切绳临界值
        /// </summary>
        private void CalculateWirelineCuttingCritical()
        {
            CuttingCriticalDictionary criticalDic = CuttingCriticalDictionaryManager.OnceInstance.DictionarySection;
            WireropeCutRole wireropeCutRole = criticalDic.WireropeCutRoles.Cast<WireropeCutRole>().FirstOrDefault(r =>
                r.MinDerrickHeight <= this.DerrickHeight.Value && r.MaxDerrickHeight >= this.DerrickHeight.Value);
            if (wireropeCutRole == null)
            {
                this.CriticalValue.Value = -1;
                return;
            }
            if (string.IsNullOrWhiteSpace(GlobalData.Wireline.Diameter.Value))
            {
                this.CriticalValue.Value = -1;
                return;
            }
            DependencyObject<WireropeWorkload> wireropeWorkload =
                this.WireropeWorkloadCollection.Items.FirstOrDefault(
                    l => l.Value.Name == GlobalData.Wireline.Diameter.Value);
            if (wireropeWorkload == null)
            {
                this.CriticalValue.Value = -1;
                return;
            }
            WireropeEfficiency wireropeEfficiency = criticalDic.WireropeEfficiencies[this.RopeCount.Value];
            if (wireropeEfficiency == null)
            {
                this.CriticalValue.Value = -1;
                return;
            }
            CommanderRopeCut commander = new CommanderRopeCut
            {
                WirelineWorkloadPerMetre = wireropeWorkload.Value.Workload,
                WirelineCutMinLength = wireropeCutRole.MinCutLength,
                WirelineCutMaxLength = wireropeCutRole.MaxCutLength,
                WirelineMaxPower = this.WirelineMaxPower.Value,
                RotaryHookWorkload = this.RotaryHookWorkload.Value,
                RopeEfficiency = wireropeEfficiency.RollingValue,
                RopeCount = this.RopeCount.Value,
                RollerDiameter = this.RollerDiameter.Value,
                WirelineDiameter = wireropeWorkload.Value.Diameter
            };
            try
            {
                this.CriticalValue.Value = commander.CalculateValue();
            }
            catch (Exception)
            {
                this.CriticalValue.Value = -1;
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
                FluidDensity = this.FluidDensity.Value,
                UnitSystemCoefficient =
                    Business.Config.ConstDictionary.UnitSystemDictionary[GlobalData.Wireline.UnitSystem.Value],
                ElevatorWeight = this.ElevatorWeight.Value,
                DrillPipeWeight = this.DrillPipes.AverageWeight,
                DrillPipeLength = this.DrillPipes.TotalLength,
                HeavierDrillPipeWeight = this.HeavierDrillPipes.AverageWeight,
                HeavierDrillPipeLength = this.HeavierDrillPipes.TotalLength,
                DrillCollarWeight = this.DrillCollars.AverageWeight,
                DrillCollarLength = this.DrillCollars.TotalLength
            };
        }

        /// <summary>
        /// 计算安全系数（服役系数）
        /// </summary>
        /// <exception cref="ConfigurationErrorsException"></exception>
        private decimal CalculateSecurityCoefficient()
        {
            CuttingCriticalDictionary criticalDic = CuttingCriticalDictionaryManager.OnceInstance.DictionarySection;
            WireropeEfficiency wireropeEfficiency = criticalDic.WireropeEfficiencies[this.RopeCount.Value];
            if (wireropeEfficiency == null)
            {
                throw new ConfigurationErrorsException("Could not find config of wirerope efficiency.");
            }
            CommanderRopeCut commander = new CommanderRopeCut
            {
                WirelineMaxPower = this.WirelineMaxPower.Value,
                RotaryHookWorkload = this.RealRotaryHookWorkload.Value,
                RopeEfficiency = wireropeEfficiency.RollingValue,
                RopeCount = this.RopeCount.Value
            };
            decimal securityCoefficient = commander.GetSecurityCoefficient();
            if (securityCoefficient > 9)
            {
                securityCoefficient = 9;
            }
            return securityCoefficient;
        }

        /// <summary>
        /// 计算钻井作业
        /// </summary>
        /// <param name="commanderOnce"></param>
        private void CalculateDrillingWork(CommanderTripOnce commanderOnce)
        {
            WorkDictionary workDic = WorkDictionaryManager.OnceInstance.DictionarySection;
            DrillingType type = workDic.DrillingTypes[this.DrillingType.Value];
            if (type == null)
            {
                this.DrillingWorkValue.Value = -1;
                return;
            }
            try
            {
                decimal securityCoefficient = CalculateSecurityCoefficient();
                CommanderDrilling commander = new CommanderDrilling(commanderOnce)
                {
                    DrivingTypeCoefficient = type.Coefficient,
                    RedressingCount = this.RedressingCount.Value,
                    DeepHeight = this.DrillingDeepHeight.Value,
                    ShallowHeight = this.DrillingShallowHeight.Value
                };
                this.DrillingWorkValue.Value = commander.CalculateValue() / securityCoefficient;
            }
            catch (Exception)
            {
                this.DrillingWorkValue.Value = -1;
            }
        }

        /// <summary>
        /// 计算起下钻作业
        /// </summary>
        /// <param name="commanderOnce"></param>
        private void CalculateTripWork(CommanderTripOnce commanderOnce)
        {
            try
            {
                decimal securityCoefficient = CalculateSecurityCoefficient();
                CommanderTrip commander = new CommanderTrip(commanderOnce)
                {
                    DeepHeight = this.TripDeepHeight.Value,
                    ShallowHeight = this.TripShallowHeight.Value,
                    Count = this.TripCount.Value
                };
                this.TripWorkValue.Value = commander.CalculateValue() / securityCoefficient;
            }
            catch (Exception)
            {
                this.TripWorkValue.Value = -1;
            }
        }

        /// <summary>
        /// 计算下套管作业
        /// </summary>
        private void CalculateBushingWork()
        {
            try
            {
                decimal securityCoefficient = CalculateSecurityCoefficient();
                CommanderBushing commander = new CommanderBushing
                {
                    FluidDensity = this.FluidDensity.Value,
                    UnitSystemCoefficient =
                        Business.Config.ConstDictionary.UnitSystemDictionary[GlobalData.Wireline.UnitSystem.Value],
                    ElevatorWeight = this.ElevatorWeight.Value,
                    BushingWeight = this.Bushings.AverageWeight,
                    BushingLength = this.Bushings.TotalLength,
                    BushingHeight = this.BushingHeight.Value
                };
                this.BushingWorkValue.Value = commander.CalculateValue() / securityCoefficient;
            }
            catch (Exception)
            {
                this.BushingWorkValue.Value = -1;
            }
        }

        /// <summary>
        /// 计算取岩心作业
        /// </summary>
        /// <param name="commanderOnce"></param>
        private void CalculateCoringWork(CommanderTripOnce commanderOnce)
        {
            try
            {
                decimal securityCoefficient = CalculateSecurityCoefficient();
                CommanderCoring commander = new CommanderCoring(commanderOnce)
                {
                    DeepHeight = this.CoringDeepHeight.Value,
                    ShallowHeight = this.CoringShallowHeight.Value
                };
                this.CoringWorkValue.Value = commander.CalculateValue() / securityCoefficient;
            }
            catch (Exception)
            {
                this.CoringWorkValue.Value = -1;
            }
        }

        /// <summary>
        /// 计算作业总和
        /// </summary>
        private void CalculateTotalWork()
        {
            if (this.DrillingWorkValue.Value < 0
                || this.TripWorkValue.Value < 0
                || this.BushingWorkValue.Value < 0
                || this.CoringWorkValue.Value < 0)
            {
                this.CumulationValue.Value = -1;
                return;
            }
            this.CumulationValue.Value = this.DrillingWorkValue.Value
                                         + this.TripWorkValue.Value
                                         + this.BushingWorkValue.Value
                                         + this.CoringWorkValue.Value;
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
                UserConfigManager.InitializeConfig();
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
        /// <param name="dbContext"></param>
        public void RefreshData(SteelWireBaseContext dbContext = null)
        {
            WirelineInfo lineInfo = null;
            CriticalRecord criticalRecord = null;
            CutRecord cutRecord = null;
            decimal totalValue = 0;
            if (!string.IsNullOrWhiteSpace(this.WirelineNumber.Value))
            {
                if (dbContext == null)
                {
                    using (dbContext = DbContextFactory.GenerateDbContext())
                    {
                        GetRefreshData(dbContext, out lineInfo, ref criticalRecord, ref cutRecord, ref totalValue);
                        GetRefreshHistory(dbContext);
                    }
                }
                else
                {
                    GetRefreshData(dbContext, out lineInfo, ref criticalRecord, ref cutRecord, ref totalValue);
                    GetRefreshHistory(dbContext);
                }
            }
            RefreshSetData(lineInfo, criticalRecord, cutRecord, totalValue);
        }

        private void GetRefreshHistory(SteelWireBaseContext dbContext)
        {
            this.History.Items.Clear();
            List<HistoryData> historyData = new List<HistoryData>();
            List<CutRecord> cutRecords = CutOperator.GetHistory(dbContext, GlobalData.SearchUserId, 30);
            historyData.AddRange(cutRecords.Select(r => new HistoryData
                (r.UpdateTime, r.WirelineInfo, r.SecurityUser, HistoryEnum.Cut, r.CutValue)));
            List<CumulationRecord> cumulationRecords = CumulationOperator.GetHistory(dbContext, GlobalData.SearchUserId, 30);
            historyData.AddRange(cumulationRecords.Select(r => new HistoryData
                (r.CalculateTime, r.CutRecord.WirelineInfo, r.SecurityUser, HistoryEnum.Cumulate, r.CumulationValue)));
            historyData = historyData.OrderByDescending(d => d.Time).Take(30).ToList();
            this.History.AddRange(historyData);
        }

        private void GetRefreshData(SteelWireBaseContext dbContext, out WirelineInfo lineInfo,
            ref CriticalRecord criticalRecord, ref CutRecord cutRecord, ref decimal totalValue)
        {
            lineInfo = WirelineOperator.GetWireline(dbContext, this.WirelineNumber.Value);
            if (lineInfo != null)
            {
                criticalRecord = CriticalOperator.GetLastRecord(dbContext, GlobalData.SearchUserId, lineInfo.ID);
                cutRecord = CutOperator.GetLastRecord(dbContext, GlobalData.SearchUserId, lineInfo.ID);
                totalValue = CutOperator.GetTotalCuttedValue(dbContext, GlobalData.SearchUserId, lineInfo.ID);
            }
        }

        /// <summary>
        /// 刷新后设置数据
        /// </summary>
        /// <param name="lineInfo"></param>
        /// <param name="criticalRecord"></param>
        /// <param name="cutRecord"></param>
        /// <param name="totalCuttedValue"></param>
        private void RefreshSetData(WirelineInfo lineInfo, CriticalRecord criticalRecord, CutRecord cutRecord,
            decimal totalCuttedValue)
        {
            if (lineInfo != null)
            {
                GlobalData.Wireline.Id = lineInfo.ID;
                GlobalData.Wireline.Number = lineInfo.Number;
                GlobalData.Wireline.Diameter.Value = lineInfo.Diameter;
                GlobalData.Wireline.Struct.Value = lineInfo.Struct;
                GlobalData.Wireline.StrongLevel.Value = lineInfo.StrongLevel;
                GlobalData.Wireline.TwistDirection.Value = lineInfo.TwistDirection;
                GlobalData.Wireline.OrderLength.Value = lineInfo.OrderLength;
                UnitSystemEnum unitSystem;
                if (Enum.TryParse(lineInfo.UnitSystem, out unitSystem))
                {
                    GlobalData.Wireline.UnitSystem.Value = unitSystem;
                }
            }
            else
            {
                GlobalData.Wireline.Id = 0;
                GlobalData.Wireline.Number = string.Empty;
            }
            GlobalData.Wireline.CriticalValue.Value = criticalRecord?.CriticalValue ?? 0;
            GlobalData.Wireline.CumulationValue.Value = cutRecord?.CumulationValue ?? 0;
            GlobalData.Wireline.TotalCuttedValue.Value = totalCuttedValue;
        }

        /// <summary>
        /// 从数据库下载配置
        /// </summary>
        public void DownLoadConfig()
        {
            // TODO 数据库下载配置信息功能未实现
        }

        private void CheckUploadData(out FileInfo criticalDicInfo, out FileInfo criticalConfigInfo,
            out FileInfo workDicInfo, out FileInfo workConfigInfo)
        {
            if (string.IsNullOrWhiteSpace(this.WirelineNumber.Value))
            {
                throw new ErrorException("CurrentWireNoEmpty");
            }
            if (!string.IsNullOrWhiteSpace(GlobalData.Wireline.Number) &&
                this.WirelineNumber.Value != GlobalData.Wireline.Number)
            {
                throw new ErrorException("CurrentWireNoInvalid");
            }
            if (string.IsNullOrWhiteSpace(GlobalData.Wireline.Struct.Value))
            {
                throw new ErrorException("CurrentWireLineStructEmpty");
            }
            if (string.IsNullOrWhiteSpace(GlobalData.Wireline.StrongLevel.Value))
            {
                throw new ErrorException("CurrentWireLineStrongLevelEmpty");
            }
            if (string.IsNullOrWhiteSpace(GlobalData.Wireline.TwistDirection.Value))
            {
                throw new ErrorException("CurrentWireLineTwistDirectionEmpty");
            }
            if (GlobalData.Wireline.OrderLength.Value <= 0)
            {
                throw new ErrorException("CurrentWireLineOrderLengthZero");
            }
            if (this.CriticalValue.Value <= 0)
            {
                throw new ErrorException("CriticalValueInvalid");
            }
            if (this.CumulationValue.Value <= 0)
            {
                throw new ErrorException("CumulationValueInvalid");
            }
            criticalDicInfo = CuttingCriticalDictionaryManager.GetConfigFile();
            if (criticalDicInfo == null)
            {
                throw new ErrorException("CriticalDictionaryFileNotFound");
            }
            criticalConfigInfo = CuttingCriticalConfigManager.GetConfigFile();
            if (criticalConfigInfo == null)
            {
                throw new ErrorException("CriticalConfigFileNotFound");
            }
            workDicInfo = WorkDictionaryManager.GetConfigFile();
            if (workDicInfo == null)
            {
                throw new ErrorException("CumulationDictionaryFileNotFound");
            }
            workConfigInfo = WorkConfigManager.GetConfigFile();
            if (workConfigInfo == null)
            {
                throw new ErrorException("CumulationConfigFileNotFound");
            }
        }

        /// <summary>
        /// 累计（上传配置和数据到数据库）
        /// </summary>
        /// <returns></returns>
        public bool Cumulate()
        {
            FileInfo criticalDicInfo, criticalConfigInfo, workDicInfo, workConfigInfo;
            CheckUploadData(out criticalDicInfo, out criticalConfigInfo, out workDicInfo, out workConfigInfo);
            DateTime now = DateTime.Now;
            using (SteelWireBaseContext dbContext = DbContextFactory.GenerateDbContext())
            {
                using (DbContextTransaction t = dbContext.Database.BeginTransaction())
                {
                    //if (WorkOperator.ExistWork(dbContext, Sign.Data.UserID, DateTime.Now))
                    //{
                    //    throw new InfoException("HaveCumulatedToday");
                    //}
                    try
                    {
                        CutRecord data = null;
                        if (GlobalData.Wireline.Id > 0)
                        {
                            data = CutOperator.GetLastRecord(dbContext, GlobalData.SearchUserId, GlobalData.Wireline.Id);
                        }
                        if (data == null)
                        {
                            if (MessageManager.Question("SaveCriticalCumulateCompulsivelyConfirm"))
                            {
                                Cumulate(dbContext, criticalDicInfo, criticalConfigInfo, workDicInfo, workConfigInfo, now);
                                dbContext.SaveChanges();
                                t.Commit();
                                return true;
                            }
                            return false;
                        }
                        if (MessageManager.Question("SaveCriticalCumulateConfirm"))
                        {
                            Cumulate(dbContext, criticalDicInfo, criticalConfigInfo, workDicInfo, workConfigInfo, now);
                            dbContext.SaveChanges();
                            t.Commit();
                            return true;
                        }
                        return false;
                    }
                    catch (Exception)
                    {
                        t.Rollback();
                        throw;
                    }
                }
            }
        }

        private void UpdateWirelineInfo(SteelWireBaseContext dbContext, out bool diameterChanged, out bool unitSystemChanged)
        {
            WirelineInfo wirelineInfo = new WirelineInfo
            {
                ID = GlobalData.Wireline.Id,
                Number = this.WirelineNumber.Value,
                Diameter = GlobalData.Wireline.Diameter.Value,
                Struct = GlobalData.Wireline.Struct.Value,
                StrongLevel = GlobalData.Wireline.StrongLevel.Value,
                TwistDirection = GlobalData.Wireline.TwistDirection.Value,
                OrderLength = GlobalData.Wireline.OrderLength.Value,
                UnitSystem = GlobalData.Wireline.UnitSystem.Value.ToString(),
                UpdateUserID = GlobalData.UserId
            };
            WirelineOperator.UpdateWireline(dbContext, wirelineInfo, out diameterChanged, out unitSystemChanged);
            GlobalData.Wireline.Id = wirelineInfo.ID;
        }

        /// <summary>
        /// 累计（上传配置和数据到数据库）
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="criticalDicInfo"></param>
        /// <param name="criticalConfigInfo"></param>
        /// <param name="workDicInfo"></param>
        /// <param name="workConfigInfo"></param>
        /// <param name="now"></param>
        private void Cumulate(SteelWireBaseContext dbContext, FileInfo criticalDicInfo, FileInfo criticalConfigInfo,
            FileInfo workDicInfo, FileInfo workConfigInfo, DateTime now)
        {
            bool lineDiameterUpdate, unitSystemUpdate;
            bool criticalConfigUpdate = this.ChangeToCriticalConfig();
            bool cumulationConfigUpdate = this.ChangeToWorkConfig();
            string wirelineNumber = string.IsNullOrWhiteSpace(GlobalData.Wireline.Number)
                ? this.WirelineNumber.Value
                : GlobalData.Wireline.Number;
            if (UserConfigManager.OnceInstance.LastWirelineNumber != wirelineNumber)
            {
                UserConfigManager.OnceInstance.LastWirelineNumber = wirelineNumber;
                UserConfigManager.OnceInstance.SaveConfig();
            }
            UpdateWirelineInfo(dbContext, out lineDiameterUpdate, out unitSystemUpdate);
            CriticalConfig configData = this.UpdateCritical(dbContext, criticalDicInfo, criticalConfigInfo,
                criticalConfigUpdate, lineDiameterUpdate, unitSystemUpdate, now);
            UpdateWork(dbContext, workDicInfo, workConfigInfo, configData, cumulationConfigUpdate, unitSystemUpdate, now);
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
            if (criticalConf.DerrickHeight != this.DerrickHeight.Value)
            {
                criticalConf.DerrickHeight = this.DerrickHeight.Value;
                changed = true;
            }
            if (criticalConf.WirelineMaxPower != this.WirelineMaxPower.Value)
            {
                criticalConf.WirelineMaxPower = this.WirelineMaxPower.Value;
                changed = true;
            }
            if (criticalConf.RotaryHookWorkload != this.RotaryHookWorkload.Value)
            {
                criticalConf.RotaryHookWorkload = this.RotaryHookWorkload.Value;
                changed = true;
            }
            if (criticalConf.RollerDiameter != this.RollerDiameter.Value)
            {
                criticalConf.RollerDiameter = this.RollerDiameter.Value;
                changed = true;
            }
            if (criticalConf.RopeCount != this.RopeCount.Value)
            {
                criticalConf.RopeCount = this.RopeCount.Value;
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
        /// <param name="criticalDicInfo"></param>
        /// <param name="criticalConfigInfo"></param>
        /// <param name="configUpdate"></param>
        /// <param name="lineDiameterUpdate"></param>
        /// <param name="unitSystemUpdate"></param>
        /// <param name="now"></param>
        private CriticalConfig UpdateCritical(SteelWireBaseContext dbContext, FileInfo criticalDicInfo, FileInfo criticalConfigInfo,
            bool configUpdate, bool lineDiameterUpdate, bool unitSystemUpdate, DateTime now)
        {
            CriticalDictionary dictionaryData;
            bool dictionaryUpdate = CriticalOperator.IsNeedUpdateCriticalDictionary(dbContext, GlobalData.SearchUserId,
                criticalDicInfo.LastWriteTime.Ticks, out dictionaryData);
            if (dictionaryUpdate)
            {
                criticalDicInfo.LastWriteTime = now;
                CuttingCriticalDictionary dictionary = CuttingCriticalDictionaryManager.OnceInstance.DictionarySection;
                dictionaryData = new CriticalDictionary
                {
                    ConfigUserID = GlobalData.UserId,
                    ConfigTime = now,
                    ConfigTimeStamp = criticalDicInfo.LastWriteTime.Ticks,
                    WireropeWorkload = dictionary.WireropeWorkloads.Cast<WireropeWorkload>()
                        .Select(w => new DbWireropeWorkload
                        {
                            Key = w.Key,
                            Name = w.Name,
                            UnitSystem = w.UnitSystem.ToString(),
                            Diameter = w.Diameter,
                            Workload = w.Workload
                        }).ToList(),
                    WireropeCutRole = dictionary.WireropeCutRoles.Cast<WireropeCutRole>()
                        .Select(w => new DbWireropeCutRole
                        {
                            Key = w.Key,
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
                CriticalOperator.UpdateCriticalDictionary(dbContext, dictionaryData);
            }
            CriticalConfig configData;
            if (!configUpdate)
            {
                configUpdate = CriticalOperator.IsNeedUpdateCriticalConfig(dbContext, GlobalData.SearchUserId,
                    criticalConfigInfo.LastWriteTime.Ticks, out configData);
            }
            if (dictionaryUpdate || configUpdate)
            {
                criticalConfigInfo.LastWriteTime = now;
                configData = new CriticalConfig
                {
                    DictionaryID = dictionaryData.ID,
                    ConfigUserID = GlobalData.UserId,
                    ConfigTime = now,
                    ConfigTimeStamp = criticalConfigInfo.LastWriteTime.Ticks,
                    DerrickHeight = this.DerrickHeight.Value,
                    WirelineMaxPower = this.WirelineMaxPower.Value,
                    RotaryHookWorkload = this.RotaryHookWorkload.Value,
                    RollerDiameter = this.RollerDiameter.Value,
                    RopeCount = this.RopeCount.Value
                };
                CriticalOperator.UpdateCriticalConfig(dbContext, configData);
            }
            else
            {
                configData = CriticalOperator.GetLastConfig(dbContext, GlobalData.SearchUserId);
            }
            bool recordUpdate = lineDiameterUpdate || unitSystemUpdate || dictionaryUpdate || configUpdate;
            decimal criticalValue = Math.Round(this.CriticalValue.Value, 8);
            if (!recordUpdate)
            {
                recordUpdate = CriticalOperator.IsNeedUpdateCriticalRecord(dbContext, GlobalData.SearchUserId,
                    GlobalData.Wireline.Id,
                    criticalValue);
            }
            if (recordUpdate)
            {
                CriticalRecord recordData = new CriticalRecord
                {
                    CriticalConfigID = configData.ID,
                    WirelineID = GlobalData.Wireline.Id,
                    CalculateUserID = GlobalData.UserId,
                    CalculateTime = now,
                    WirelineDiameter = GlobalData.Wireline.Diameter.Value,
                    CriticalValue = criticalValue
                };
                CriticalOperator.UpdateCriticalRecord(dbContext, recordData);
            }
            return configData;
        }

        /// <summary>
        /// 保存到本地配置文件
        /// </summary>
        /// <param name="drills"></param>
        /// <param name="configs"></param>
        /// <returns></returns>
        private bool ChangeDrillList(AddCollection<Drill> drills,
            IEnumerable<DependencyObject<DependencyDrillConfig>> configs)
        {
            bool changed = false;
            List<Drill> remove =
                drills.Cast<Drill>().Where(d => configs.All(p => p.Value.Name.Value != d.Name)).ToList();
            if (remove.Count > 0)
            {
                foreach (Drill drill in remove)
                {
                    drills.Remove(drill);
                }
                changed = true;
            }
            foreach (DependencyObject<DependencyDrillConfig> config in configs)
            {
                Drill item = drills[config.Value.Name.Value];
                if (item == null)
                {
                    item = new Drill
                    {
                        Name = config.Value.Name.Value,
                        Weight = config.Value.Weight.Value,
                        Length = config.Value.Length.Value
                    };
                    drills.Add(item);
                    changed = true;
                }
                else
                {
                    if (item.Weight != config.Value.Weight.Value)
                    {
                        item.Weight = config.Value.Weight.Value;
                        changed = true;
                    }
                    if (item.Length != config.Value.Length.Value)
                    {
                        item.Length = config.Value.Length.Value;
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

            if (workConf.RealRotaryHookWorkload != this.RealRotaryHookWorkload.Value)
            {
                workConf.RealRotaryHookWorkload = this.RealRotaryHookWorkload.Value;
                changed = true;
            }
            if (workConf.FluidDensity != this.FluidDensity.Value)
            {
                workConf.FluidDensity = this.FluidDensity.Value;
                changed = true;
            }
            if (workConf.ElevatorWeight != this.ElevatorWeight.Value)
            {
                workConf.ElevatorWeight = this.ElevatorWeight.Value;
                changed = true;
            }
            if (ChangeDrillList(workConf.DrillPipes, this.DrillPipes.Items))
            {
                changed = true;
            }
            if (ChangeDrillList(workConf.HeavierDrillPipes, this.HeavierDrillPipes.Items))
            {
                changed = true;
            }
            if (ChangeDrillList(workConf.DrillCollars, this.DrillCollars.Items))
            {
                changed = true;
            }

            #endregion

            #region Drilling

            if (workConf.DrillingShallowHeight != this.DrillingShallowHeight.Value)
            {
                workConf.DrillingShallowHeight = this.DrillingShallowHeight.Value;
                changed = true;
            }
            if (workConf.DrillingDeepHeight != this.DrillingDeepHeight.Value)
            {
                workConf.DrillingDeepHeight = this.DrillingDeepHeight.Value;
                changed = true;
            }
            if (workConf.DrillingType != this.DrillingType.Value)
            {
                workConf.DrillingType = this.DrillingType.Value;
                changed = true;
            }
            if (workConf.RedressingCount != this.RedressingCount.Value)
            {
                workConf.RedressingCount = this.RedressingCount.Value;
                changed = true;
            }

            #endregion

            #region Trip

            if (workConf.TripShallowHeight != this.TripShallowHeight.Value)
            {
                workConf.TripShallowHeight = this.TripShallowHeight.Value;
                changed = true;
            }
            if (workConf.TripDeepHeight != this.TripDeepHeight.Value)
            {
                workConf.TripDeepHeight = this.TripDeepHeight.Value;
                changed = true;
            }
            if (workConf.TripCount != this.TripCount.Value)
            {
                workConf.TripCount = this.TripCount.Value;
                changed = true;
            }

            #endregion

            #region Bushing

            if (ChangeDrillList(workConf.Bushings, this.Bushings.Items))
            {
                changed = true;
            }
            if (workConf.BushingHeight != BushingHeight.Value)
            {
                workConf.BushingHeight = BushingHeight.Value;
                changed = true;
            }

            #endregion

            #region Coring

            if (workConf.CoringShallowHeight != CoringShallowHeight.Value)
            {
                workConf.CoringShallowHeight = CoringShallowHeight.Value;
                changed = true;
            }
            if (workConf.CoringDeepHeight != CoringDeepHeight.Value)
            {
                workConf.CoringDeepHeight = CoringDeepHeight.Value;
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
        /// <param name="workDicInfo"></param>
        /// <param name="workConfigInfo"></param>
        /// <param name="criticalConfigData"></param>
        /// <param name="configUpdate"></param>
        /// <param name="unitSystemUpdate"></param>
        /// <param name="now"></param>
        private void UpdateWork(SteelWireBaseContext dbContext, FileInfo workDicInfo, FileInfo workConfigInfo,
            CriticalConfig criticalConfigData, bool configUpdate, bool unitSystemUpdate, DateTime now)
        {
            CumulationDictionary dictionaryData;
            bool dictionaryUpdate = CumulationOperator.IsNeedUpdateCumulationDictionary(dbContext,
                GlobalData.SearchUserId,
                workDicInfo.LastWriteTime.Ticks, out dictionaryData);
            if (dictionaryUpdate)
            {
                workDicInfo.LastWriteTime = now;
                WorkDictionary dictionary = WorkDictionaryManager.OnceInstance.DictionarySection;
                dictionaryData = new CumulationDictionary
                {
                    ConfigUserID = GlobalData.UserId,
                    ConfigTime = now,
                    ConfigTimeStamp = workDicInfo.LastWriteTime.Ticks,
                    DrillingType = dictionary.DrillingTypes.Cast<DrillingType>()
                        .Select(d => new DbDrillingType
                        {
                            Name = d.Name.ToString(),
                            Coefficient = d.Coefficient
                        }).ToList()
                };
                CumulationOperator.UpdateCumulationDictionary(dbContext, dictionaryData);
            }
            CumulationConfig configData;
            if (!configUpdate)
            {
                configUpdate = CumulationOperator.IsNeedUpdateCumulationConfig(dbContext, GlobalData.SearchUserId,
                    workConfigInfo.LastWriteTime.Ticks, out configData);
            }
            if (dictionaryUpdate || configUpdate)
            {
                workConfigInfo.LastWriteTime = now;
                configData = new CumulationConfig
                {
                    ConfigUserID = GlobalData.UserId,
                    DictionaryID = dictionaryData.ID,
                    ConfigTime = now,
                    ConfigTimeStamp = workConfigInfo.LastWriteTime.Ticks,
                    RealRotaryHookWorkload = this.RealRotaryHookWorkload.Value,
                    FluidDensity = this.FluidDensity.Value,
                    ElevatorWeight = this.ElevatorWeight.Value,
                    DrillDeviceConfig = this.DrillPipes.Items
                        .Where(d => d.Value.Weight.Value > 0 && d.Value.Length.Value > 0)
                        .Select(d => new DrillDeviceConfig
                        {
                            Name = d.Value.Name.Value,
                            Weight = d.Value.Weight.Value,
                            Length = d.Value.Length.Value,
                            Type = DrillDeviceTypeEnum.DrillPipe.ToString()
                        })
                        .Union(this.HeavierDrillPipes.Items
                            .Where(d => d.Value.Weight.Value > 0 && d.Value.Length.Value > 0)
                            .Select(d => new DrillDeviceConfig
                            {
                                Name = d.Value.Name.Value,
                                Weight = d.Value.Weight.Value,
                                Length = d.Value.Length.Value,
                                Type = DrillDeviceTypeEnum.HeavierDrillPipe.ToString()
                            }))
                        .Union(this.DrillCollars.Items
                            .Where(d => d.Value.Weight.Value > 0 && d.Value.Length.Value > 0)
                            .Select(d => new DrillDeviceConfig
                            {
                                Name = d.Value.Name.Value,
                                Weight = d.Value.Weight.Value,
                                Length = d.Value.Length.Value,
                                Type = DrillDeviceTypeEnum.DrillCollar.ToString()
                            }))
                        .Union(this.Bushings.Items
                            .Where(d => d.Value.Weight.Value > 0 && d.Value.Length.Value > 0)
                            .Select(d => new DrillDeviceConfig
                            {
                                Name = d.Value.Name.Value,
                                Weight = d.Value.Weight.Value,
                                Length = d.Value.Length.Value,
                                Type = DrillDeviceTypeEnum.Bushing.ToString()
                            })).ToList(),
                    DrillingShallowHeight = this.DrillingShallowHeight.Value,
                    DrillingDeepHeight = this.DrillingDeepHeight.Value,
                    DrillingType = this.DrillingType.Value.ToString(),
                    RedressingCount = this.RedressingCount.Value,

                    TripShallowHeight = this.TripShallowHeight.Value,
                    TripDeepHeight = this.TripDeepHeight.Value,
                    TripCount = this.TripCount.Value,

                    BushingHeight = this.BushingHeight.Value,

                    CoringShallowHeight = this.CoringShallowHeight.Value,
                    CoringDeepHeight = this.CoringDeepHeight.Value
                };
                CumulationOperator.UpdateCumulationConfig(dbContext, configData);
            }
            else
            {
                configData = CumulationOperator.GetLastConfig(dbContext, GlobalData.SearchUserId);
            }
            if (unitSystemUpdate)
            {
                CutOperator.CalculateAllRecord(dbContext, GlobalData.SearchUserId, GlobalData.UserId,
                    GlobalData.Wireline.Id,
                    now);
            }
            CumulationRecord recordData = new CumulationRecord
            {
                CalculateUserID = GlobalData.UserId,
                CriticalConfigID = criticalConfigData.ID,
                CumulationConfigID = configData.ID,
                CalculateTime = now,
                CumulationValue = Math.Round(this.CumulationValue.Value, 8)
            };
            CumulationOperator.UpdateCumulationRecord(dbContext, GlobalData.SearchUserId, GlobalData.Wireline.Id,
                recordData);
        }

        /// <summary>
        /// 判断是否需要切绳（精确到小数3位）
        /// </summary>
        /// <returns></returns>
        public bool CheckNeedReset()
        {
            return GlobalData.Wireline.CriticalValue.Value > 0
                   && Math.Round(GlobalData.Wireline.CumulationValue.Value, 3) >=
                   Math.Round(GlobalData.Wireline.CriticalValue.Value, 3);
        }

        private void CheckReset(bool isWarningMode)
        {
            if (string.IsNullOrWhiteSpace(this.WirelineNumber.Value))
            {
                throw new ErrorException("CurrentWireNoEmpty");
            }
            if (this.WirelineNumber.Value != GlobalData.Wireline.Number)
            {
                throw new ErrorException("CurrentWireNoInvalid");
            }
            if (GlobalData.Wireline.CumulationValue.Value <= 0)
            {
                throw new ErrorException("CurrentCumulationValueZero");
            }
            if (isWarningMode)
            {
                if (this.CutLengthValue.Value <= 0)
                {
                    throw new InfoException("InfoCutLengthZero");
                }
            }
            else
            {
                if (this.CutLengthValue.Value <= 0)
                {
                    throw new ErrorException("CutLengthZero");
                }
            }
        }

        /// <summary>
        /// 切绳并上传数据
        /// </summary>
        /// <param name="isWarningMode"></param>
        /// <returns></returns>
        public void Reset(bool isWarningMode)
        {
            CheckReset(isWarningMode);
            using (SteelWireBaseContext dbContext = DbContextFactory.GenerateDbContext())
            {
                //if (ResetOperator.ExistReset(dbContext, Sign.Data.UserID, DateTime.Now))
                //{
                //    throw new InfoException("HaveResetToday");
                //}
                CutRecord recordData = CutOperator.GetLastRecord(dbContext, GlobalData.SearchUserId,
                    GlobalData.Wireline.Id);
                if (recordData == null)
                {
                    throw new ErrorException("CumulationDataNotFound");
                }
                if (isWarningMode)
                {
                    if (MessageManager.Question("ResetCompulsivelyConfirm"))
                    {
                        Reset(dbContext, recordData);
                        return;
                    }
                    return;
                }
                if (MessageManager.Question("ResetConfirm"))
                {
                    Reset(dbContext, recordData);
                }
            }
        }

        /// <summary>
        /// 切绳并上传数据
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="recordData"></param>
        private void Reset(SteelWireBaseContext dbContext, CutRecord recordData)
        {
            CutOperator.Cut(dbContext, GlobalData.SearchUserId, GlobalData.UserId, GlobalData.Wireline.Id,
                this.CutLengthValue.Value, recordData);
            dbContext.SaveChanges();
            RefreshData(dbContext);
        }
    }
}