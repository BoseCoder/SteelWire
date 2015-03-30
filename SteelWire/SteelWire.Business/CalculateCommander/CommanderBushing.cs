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

        public CommanderBushing()
            : base(0)
        { }

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
            return (this.BushingWeight * temp * (this.BushingLength + this.BushingHeight) / 1000 + this.ElevatorWeight / 250) * this.BushingHeight / 2000;
        }
    }
}
