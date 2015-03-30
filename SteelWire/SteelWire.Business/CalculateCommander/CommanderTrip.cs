using System;

namespace SteelWire.Business.CalculateCommander
{
    /// <summary>
    /// 起下钻作业钢丝绳吨公里计算
    /// </summary>
    public class CommanderTrip : CommanderBase
    {
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
        /// 起下钻次数
        /// </summary>
        public decimal Count { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="commanderTripOnce">一次起下钻</param>
        public CommanderTrip(CommanderTripOnce commanderTripOnce)
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
            return (deep - shallow) * this.Count;
        }
    }
}
