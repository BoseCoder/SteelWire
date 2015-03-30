namespace SteelWire.Business.CalculateCommander
{
    public abstract class CommanderBase
    {
        /// <summary>
        /// 计算结果
        /// </summary>
        public decimal Result { get; private set; }

        protected CommanderBase(decimal value)
        {
            this.Result = value;
        }

        /// <summary>
        /// 检查输入
        /// </summary>
        /// <returns></returns>
        protected abstract void CheckInput();

        /// <summary>
        /// 计算公式
        /// </summary>
        /// <returns></returns>
        protected abstract decimal Calculate();

        /// <summary>
        /// 计算
        /// </summary>
        /// <returns></returns>
        public virtual decimal CalculateValue()
        {
            this.CheckInput();
            this.Result = this.Calculate();
            return this.Result;
        }
    }
}
