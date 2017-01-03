using System;

namespace SteelWire.Business.CalculateCommander
{
    /// <summary>
    /// 钻井作业钢丝绳吨公里计算
    /// </summary>
    public class CommanderDrilling : CommanderBase
    {
        /// <summary>
        /// 一次起下钻计算
        /// </summary>
        private readonly CommanderTripOnce _commanderTripOnce;

        /// <summary>
        /// 钻机驱动方式
        /// </summary>
        public decimal DrivingTypeCoefficient { get; set; }
        /// <summary>
        /// 划眼次数
        /// </summary>
        public long RedressingCount { get; set; }
        /// <summary>
        /// 较深钻井深度
        /// </summary>
        public decimal DeepHeight { get; set; }
        /// <summary>
        /// 较浅钻井深度
        /// </summary>
        public decimal ShallowHeight { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="commanderTripOnce">一次起下钻</param>
        public CommanderDrilling(CommanderTripOnce commanderTripOnce)
        {
            if (commanderTripOnce == null)
            {
                throw new ArgumentNullException(nameof(commanderTripOnce));
            }
            this._commanderTripOnce = commanderTripOnce;
        }

        /// <summary>
        /// 检查输入
        /// </summary>
        protected override void CheckInput()
        {

        }

        /// <summary>
        /// 计算公式
        /// </summary>
        /// <returns></returns>
        protected override decimal Calculate()
        {
            this._commanderTripOnce.DrillingHeight = this.DeepHeight;
            decimal deep = this._commanderTripOnce.CalculateValue();
            this._commanderTripOnce.DrillingHeight = this.ShallowHeight;
            decimal shallow = this._commanderTripOnce.CalculateValue();
            return (this.DrivingTypeCoefficient + this.RedressingCount) * (deep - shallow);
        }
    }
}
