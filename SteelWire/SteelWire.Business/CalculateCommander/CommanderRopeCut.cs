using System;

namespace SteelWire.Business.CalculateCommander
{
    /// <summary>
    /// 切绳吨公里计算
    /// </summary>
    public class CommanderRopeCut : CommanderBase
    {
        /// <summary>
        /// 安全系数最大值为9
        /// </summary>
        const decimal SecurityCoefficientMaxValue = 9;
        /// <summary>
        /// 钢丝绳每米工作量
        /// </summary>
        public decimal WirelineWorkloadPerMetre { get; set; }
        /// <summary>
        /// 切绳长度最小值
        /// </summary>
        public decimal WirelineCutMinLength { get; set; }
        /// <summary>
        /// 切绳长度最大值
        /// </summary>
        public decimal WirelineCutMaxLength { get; set; }
        /// <summary>
        /// 切绳长度
        /// </summary>
        public decimal WirelineCutLength { get; private set; }
        /// <summary>
        /// 钢丝绳破断拉力
        /// </summary>
        public decimal WirelineMaxPower { get; set; }
        /// <summary>
        /// 快绳拉力
        /// </summary>
        public decimal FastLinePower { get; private set; }
        /// <summary>
        /// 大钩载荷
        /// </summary>
        public decimal RotaryHookWorkload { get; set; }
        /// <summary>
        /// 缠绳效率
        /// </summary>
        public decimal RopeEfficiency { get; set; }
        /// <summary>
        /// 承载绳根数
        /// </summary>
        public long RopeCount { get; set; }
        /// <summary>
        /// 安全系数是否限制为9
        /// </summary>
        public bool SecurityCoefficientLimitNine { get; set; }
        /// <summary>
        /// 安全系数修正系数
        /// </summary>
        public decimal SecurityCorrectionCoefficient { get; private set; }
        /// <summary>
        /// 滚筒直径
        /// </summary>
        public decimal RollerDiameter { get; set; }
        /// <summary>
        /// 钢丝绳公称直径
        /// </summary>
        public decimal WirelineDiameter { get; set; }
        /// <summary>
        /// 滑轮D:d比率修正系数
        /// </summary>
        public decimal PulleyCoefficient { get; private set; }

        /// <summary>
        /// 检查输入
        /// </summary>
        protected override void CheckInput()
        {
            CheckInputForCalculateWirelineCutLength();
            CheckInputForCalculateFastLinePower();
            CheckInputForCalculatePulleyCoefficient();
        }

        /// <summary>
        /// 计算公式
        /// </summary>
        /// <returns></returns>
        protected override decimal Calculate()
        {
            this.WirelineCutLength = CalculateWirelineCutLength();
            this.FastLinePower = CalculateFastLinePower();
            this.SecurityCorrectionCoefficient = CalculateSecurityCoefficient();
            this.PulleyCoefficient = CalculatePulleyCoefficient();
            return this.WirelineWorkloadPerMetre * this.WirelineCutLength * this.SecurityCorrectionCoefficient * this.PulleyCoefficient;
        }

        #region WirelineCutLength

        /// <summary>
        /// 计算切绳长度前检查输入
        /// </summary>
        private void CheckInputForCalculateWirelineCutLength()
        {

        }

        /// <summary>
        /// 计算切绳长度
        /// </summary>
        private decimal CalculateWirelineCutLength()
        {
            return (this.WirelineCutMinLength + this.WirelineCutMaxLength) / 2;
        }

        /// <summary>
        /// 计算切绳长度
        /// </summary>
        /// <returns></returns>
        public decimal GetWirelineCutLength()
        {
            CheckInputForCalculateWirelineCutLength();
            return CalculateWirelineCutLength();
        }

        #endregion

        #region FastLinePower

        /// <summary>
        /// 计算快绳拉力前检查输入
        /// </summary>
        private void CheckInputForCalculateFastLinePower()
        {
            if (this.RopeEfficiency == decimal.Zero)
            {
                throw new DivideByZeroException("RopeEfficiency is invalid.");
            }
            if (this.RopeCount == 0)
            {
                throw new DivideByZeroException("RopeCount is invalid.");
            }
            if (this.RotaryHookWorkload == decimal.Zero)
            {
                throw new DivideByZeroException("RotaryHookWorkload is invalid.");
            }
        }

        /// <summary>
        /// 计算快绳拉力
        /// </summary>
        private decimal CalculateFastLinePower()
        {
            return this.RotaryHookWorkload / (this.RopeEfficiency * this.RopeCount);
        }

        /// <summary>
        /// 计算快绳拉力
        /// </summary>
        public decimal GetFastLinePower()
        {
            CheckInputForCalculateFastLinePower();
            return CalculateFastLinePower();
        }

        #endregion

        #region SecurityCoefficient

        /// <summary>
        /// 计算安全系数修正系数前检查输入
        /// </summary>
        private void CheckInputForCalculateSecurityCoefficient()
        {
            if (this.FastLinePower == decimal.Zero)
            {
                throw new DivideByZeroException("FastLinePower is invalid.");
            }
        }

        /// <summary>
        /// 计算安全系数修正系数
        /// </summary>
        /// <returns></returns>
        private decimal CalculateSecurityCoefficient()
        {
            decimal coefficient = this.WirelineMaxPower / this.FastLinePower;
            if (this.SecurityCoefficientLimitNine && coefficient > SecurityCoefficientMaxValue)
            {
                coefficient = SecurityCoefficientMaxValue;
            }
            decimal pow = (decimal)Math.Pow((double)coefficient, 2);
            return -0.016M * pow + 0.344M * coefficient - 0.326M;
        }

        /// <summary>
        /// 计算安全系数修正系数
        /// </summary>
        /// <returns></returns>
        public decimal GetSecurityCoefficient()
        {
            CheckInputForCalculateFastLinePower();
            this.FastLinePower = CalculateFastLinePower();
            CheckInputForCalculateSecurityCoefficient();
            return CalculateSecurityCoefficient();
        }

        #endregion

        #region PulleyCoefficient

        /// <summary>
        /// 计算滑轮D:d比率修正系数前检查输入
        /// </summary>
        private void CheckInputForCalculatePulleyCoefficient()
        {
            if (this.WirelineDiameter == decimal.Zero)
            {
                throw new DivideByZeroException("WirelineDiameter is invalid.");
            }
        }

        /// <summary>
        /// 计算滑轮D:d比率修正系数
        /// </summary>
        /// <returns></returns>
        private decimal CalculatePulleyCoefficient()
        {
            decimal coefficient = this.RollerDiameter / this.WirelineDiameter;
            decimal pow = (decimal)Math.Pow((double)coefficient, 2);
            return 0.00395M * pow - 0.0326M * coefficient + 0.0724M;
        }

        /// <summary>
        /// 计算滑轮D:d比率修正系数
        /// </summary>
        /// <returns></returns>
        public decimal GetPulleyCoefficient()
        {
            CheckInputForCalculatePulleyCoefficient();
            return CalculatePulleyCoefficient();
        }

        #endregion
    }
}
