using System;
using SteelWire.Business.Config;

namespace SteelWire.Business.CalculateCommander
{
    /// <summary>
    /// 下套管作业钢丝绳吨公里计算
    /// </summary>
    public class CommanderBushing : CommanderBase
    {
        /// <summary>
        /// 钻井液密度
        /// </summary>
        public decimal FluidDensity { get; set; }
        /// <summary>
        /// 单位体制系数
        /// </summary>
        public UnitSystemCoefficient UnitSystemCoefficient { get; set; }
        /// <summary>
        /// 游车-吊卡总成的总质量
        /// </summary>
        public decimal ElevatorWeight { get; set; }
        /// <summary>
        /// 套管公称质量
        /// </summary>
        public decimal BushingWeight { get; set; }
        /// <summary>
        /// 套管单根长度
        /// </summary>
        public decimal BushingLength { get; set; }
        /// <summary>
        /// 下套管深度
        /// </summary>
        public decimal BushingHeight { get; set; }

        /// <summary>
        /// 检查输入
        /// </summary>
        protected override void CheckInput()
        {
            if (this.UnitSystemCoefficient == null)
            {
                throw new ArgumentException("UnitSystemCoefficient could not be null.");
            }
        }

        /// <summary>
        /// 计算公式
        /// </summary>
        /// <returns></returns>
        protected override decimal Calculate()
        {
            decimal temp = 1 - this.FluidDensity * this.UnitSystemCoefficient.FluidDensityCoefficient;
            return (this.BushingWeight * temp * (this.BushingLength + this.BushingHeight) / this.UnitSystemCoefficient.LeftDenominatorCoefficient + this.ElevatorWeight / this.UnitSystemCoefficient.RightDenominatorCoefficient) * this.BushingHeight / 2;
        }
    }
}
