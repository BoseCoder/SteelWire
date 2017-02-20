using System;
using System.Collections.Generic;
using System.Linq;
using SteelWire.Business.Config;
using SteelWire.Business.Database;

namespace SteelWire.Business.CalculateCommander
{
    public class CommanderCumulation : CommanderBase
    {
        private readonly WirelineInfo _wirelineInfo;
        private readonly CriticalConfig _criticalConfig;
        private readonly WireropeEfficiency _wireropeEfficiency;
        private readonly CumulationConfig _cumulationConfig;
        private readonly List<DrillDeviceConfig> _drillDeviceConfigs;
        private readonly DrillingType _drillingType;
        private UnitSystemEnum _unitSystem;

        public CommanderCumulation(WirelineInfo wirelineInfo, CriticalConfig criticalConfig, WireropeEfficiency wireropeEfficiency,
            CumulationConfig cumulationConfig, List<DrillDeviceConfig> drillDeviceConfigs, DrillingType drillingType)
        {
            if (wirelineInfo == null)
            {
                throw new ArgumentNullException(nameof(wirelineInfo));
            }
            if (criticalConfig == null)
            {
                throw new ArgumentNullException(nameof(criticalConfig));
            }
            if (wireropeEfficiency == null)
            {
                throw new ArgumentNullException(nameof(wireropeEfficiency));
            }
            if (cumulationConfig == null)
            {
                throw new ArgumentNullException(nameof(cumulationConfig));
            }
            if (drillDeviceConfigs == null)
            {
                throw new ArgumentNullException(nameof(drillDeviceConfigs));
            }
            if (drillingType == null)
            {
                throw new ArgumentNullException(nameof(drillingType));
            }
            this._wirelineInfo = wirelineInfo;
            this._criticalConfig = criticalConfig;
            this._wireropeEfficiency = wireropeEfficiency;
            this._cumulationConfig = cumulationConfig;
            this._drillDeviceConfigs = drillDeviceConfigs;
            this._drillingType = drillingType;
        }

        protected override void CheckInput()
        {
            if (!Enum.TryParse(this._wirelineInfo.UnitSystem, out this._unitSystem))
            {
                throw new ArgumentException("UnitSystem of wirelineInfo is invalid.", nameof(this._wirelineInfo));
            }
        }

        protected override decimal Calculate()
        {
            CommanderRopeCut commanderRopeCut = new CommanderRopeCut
            {
                SecurityCoefficientLimitNine = true,
                WirelineMaxPower = this._criticalConfig.WirelineMaxPower,
                RotaryHookWorkload = this._cumulationConfig.RealRotaryHookWorkload,
                RopeEfficiency = this._wireropeEfficiency.RollingValue,
                RopeCount = this._criticalConfig.RopeCount
            };
            decimal securityCoefficient = commanderRopeCut.GetSecurityCoefficient();
            List<DrillDeviceConfig> drillPipes =
                this._drillDeviceConfigs.Where(c => c.Type == DrillDeviceTypeEnum.DrillPipe.ToString()).ToList();
            List<DrillDeviceConfig> heavierDrillPipes =
                this._drillDeviceConfigs.Where(c => c.Type == DrillDeviceTypeEnum.HeavierDrillPipe.ToString()).ToList();
            List<DrillDeviceConfig> drillCollars =
                this._drillDeviceConfigs.Where(c => c.Type == DrillDeviceTypeEnum.DrillCollar.ToString()).ToList();
            List<DrillDeviceConfig> drillBushings =
                this._drillDeviceConfigs.Where(c => c.Type == DrillDeviceTypeEnum.Bushing.ToString()).ToList();
            CommanderTripOnce commanderTripOnce = new CommanderTripOnce
            {
                FluidDensity = this._cumulationConfig.FluidDensity,
                UnitSystemCoefficient = ConstDictionary.UnitSystemDictionary[this._unitSystem],
                ElevatorWeight = this._cumulationConfig.ElevatorWeight,
                DrillPipeLength = drillPipes.Sum(d => d.Length),
                HeavierDrillPipeLength = heavierDrillPipes.Sum(d => d.Length),
                DrillCollarLength = drillCollars.Sum(d => d.Length)
            };
            commanderTripOnce.DrillPipeWeight = drillPipes.Sum(d => d.Weight * d.Length) / commanderTripOnce.DrillPipeLength;
            commanderTripOnce.HeavierDrillPipeWeight = heavierDrillPipes.Sum(d => d.Weight * d.Length) / commanderTripOnce.HeavierDrillPipeLength;
            commanderTripOnce.DrillCollarWeight = drillCollars.Sum(d => d.Weight * d.Length) / commanderTripOnce.DrillCollarLength;
            CommanderDrilling commanderDrilling = new CommanderDrilling(commanderTripOnce)
            {
                DrivingTypeCoefficient = this._drillingType.Coefficient,
                RedressingCount = this._cumulationConfig.RedressingCount,
                DeepHeight = this._cumulationConfig.DrillingDeepHeight,
                ShallowHeight = this._cumulationConfig.DrillingShallowHeight
            };
            CommanderTrip commanderTrip = new CommanderTrip(commanderTripOnce)
            {
                DeepHeight = this._cumulationConfig.TripDeepHeight,
                ShallowHeight = this._cumulationConfig.TripShallowHeight,
                Count = this._cumulationConfig.TripCount
            };
            CommanderBushing commanderBushing = new CommanderBushing
            {
                FluidDensity = this._cumulationConfig.FluidDensity,
                UnitSystemCoefficient = ConstDictionary.UnitSystemDictionary[this._unitSystem],
                ElevatorWeight = this._cumulationConfig.ElevatorWeight,
                BushingLength = drillBushings.Sum(d => d.Length),
                BushingHeight = this._cumulationConfig.BushingHeight
            };
            commanderBushing.BushingWeight = drillBushings.Sum(d => d.Weight * d.Length) / commanderBushing.BushingLength;
            CommanderCoring commanderCoring = new CommanderCoring(commanderTripOnce)
            {
                DeepHeight = this._cumulationConfig.CoringDeepHeight,
                ShallowHeight = this._cumulationConfig.CoringShallowHeight
            };
            return (commanderDrilling.CalculateValue() +
                    commanderTrip.CalculateValue() +
                    commanderBushing.CalculateValue() +
                    commanderCoring.CalculateValue()) / securityCoefficient;
        }
    }
}
