namespace SteelWire.Business.CalculateCommander
{
    /// <summary>
    /// 一次起下钻作业钢丝绳吨公里计算
    /// </summary>
    public class CommanderTripOnce : CommanderBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public CommanderTripOnce()
            : base(0)
        { }

        
        /// <summary>
        /// 钻井液密度
        /// </summary>
        public decimal FluidDensity { get; set; }
        /// <summary>
        /// 游车-吊卡总成的总质量
        /// </summary>
        public decimal ElevatorWeight { get; set; }
        /// <summary>
        /// 钻杆公称质量
        /// </summary>
        public decimal DrillPipeWeight { get; set; }
        /// <summary>
        /// 钻杆立根长度
        /// </summary>
        public decimal DrillPipeLength { get; set; }
        /// <summary>
        /// 钻铤公称质量
        /// </summary>
        public decimal DrillCollarWeight { get; set; }
        /// <summary>
        /// 钻铤长度
        /// </summary>
        public decimal DrillCollarLength { get; set; }
        /// <summary>
        /// 起下钻深度
        /// </summary>
        public decimal DrillingHeight { get; set; }

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
            decimal temp = (7.856M - this.FluidDensity) / 7.856M;
            decimal left = this.DrillPipeWeight * temp * (this.DrillPipeLength + this.DrillingHeight) / 1000;
            decimal right = (this.ElevatorWeight + 0.5M * (this.DrillCollarWeight - this.DrillPipeWeight) * this.DrillCollarLength * temp) / 250;
            return (left + right) * this.DrillingHeight / 1000;
        }
    }
}
