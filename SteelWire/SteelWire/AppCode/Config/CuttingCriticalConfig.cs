using System.Configuration;

namespace SteelWire.AppCode.Config
{
    /// <summary>
    /// 切绳临界值当前配置
    /// </summary>
    public class CuttingCriticalConfig : ConfigurationSection
    {
        /// <summary>
        /// 井架高度
        /// </summary>
        [ConfigurationProperty("derrickHeight", IsRequired = true)]
        public decimal DerrickHeight
        {
            get { return (decimal)this["derrickHeight"]; }
            set { this["derrickHeight"] = value; }
        }
        /// <summary>
        /// 钢丝绳破断拉力
        /// </summary>
        [ConfigurationProperty("wirelineMaxPower", IsRequired = true)]
        public decimal WirelineMaxPower
        {
            get { return (decimal)this["wirelineMaxPower"]; }
            set { this["wirelineMaxPower"] = value; }
        }
        /// <summary>
        /// 大钩载荷
        /// </summary>
        [ConfigurationProperty("rotaryHookWorkload", IsRequired = true)]
        public decimal RotaryHookWorkload
        {
            get { return (decimal)this["rotaryHookWorkload"]; }
            set { this["rotaryHookWorkload"] = value; }
        }
        /// <summary>
        /// 滚筒直径
        /// </summary>
        [ConfigurationProperty("rollerDiameter", IsRequired = true)]
        public decimal RollerDiameter
        {
            get { return (decimal)this["rollerDiameter"]; }
            set { this["rollerDiameter"] = value; }
        }
        /// <summary>
        /// 承载绳根数
        /// </summary>
        [ConfigurationProperty("ropeCount", IsRequired = true)]
        public long RopeCount
        {
            get { return (long)this["ropeCount"]; }
            set { this["ropeCount"] = value; }
        }
    }
}