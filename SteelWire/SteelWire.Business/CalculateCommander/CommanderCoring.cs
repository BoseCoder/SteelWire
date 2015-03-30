using System;

namespace SteelWire.Business.CalculateCommander
{
    /// <summary>
    /// 取岩心作业钢丝绳吨公里计算
    /// </summary>
    public class CommanderCoring : CommanderBase
    {
        /// <summary>
        /// 常量系数
        /// </summary>
        private const int CommonCoefficient = 2;
        /// <summary>
        /// 一次起下钻计算
        /// </summary>
        private readonly CommanderTripOnce _commanderTripOnce;
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
        public CommanderCoring(CommanderTripOnce commanderTripOnce)
            : base(0)
        {
            if (commanderTripOnce == null)
            {
                throw new ArgumentNullException("commanderTripOnce");
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
            return CommonCoefficient * (deep - shallow);
        }
    }
}
