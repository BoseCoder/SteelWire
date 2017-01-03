namespace SteelWire.Business.CalculateCommander
{
    public abstract class CommanderBase
    {
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
            return this.Calculate();
        }
    }
}
