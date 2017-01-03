using System.Configuration;
using BaseConfig;

namespace SteelWire.AppCode.Config
{
    /// <summary>
    /// 钢丝绳吨公里计算当前配置
    /// </summary>
    public class WorkConfig : ConfigurationSection
    {
        /// <summary>
        /// 当班大钩显示悬重
        /// </summary>
        [ConfigurationProperty("realRotaryHookWorkload", IsRequired = true)]
        public decimal RealRotaryHookWorkload
        {
            get { return (decimal)this["realRotaryHookWorkload"]; }
            set { this["realRotaryHookWorkload"] = value; }
        }
        /// <summary>
        /// 钻杆
        /// </summary>
        [ConfigurationProperty("DrillPipes", IsRequired = true)]
        public AddCollection<Drill> DrillPipes
        {
            get { return (AddCollection<Drill>)this["DrillPipes"]; }
            set { this["DrillPipes"] = value; }
        }
        /// <summary>
        /// 加重钻杆
        /// </summary>
        [ConfigurationProperty("HeavierDrillPipes", IsRequired = true)]
        public AddCollection<Drill> HeavierDrillPipes
        {
            get { return (AddCollection<Drill>)this["HeavierDrillPipes"]; }
            set { this["HeavierDrillPipes"] = value; }
        }
        /// <summary>
        /// 钻铤
        /// </summary>
        [ConfigurationProperty("DrillCollars", IsRequired = true)]
        public AddCollection<Drill> DrillCollars
        {
            get { return (AddCollection<Drill>)this["DrillCollars"]; }
            set { this["DrillCollars"] = value; }
        }
        /// <summary>
        /// 套管
        /// </summary>
        [ConfigurationProperty("Bushings", IsRequired = true)]
        public AddCollection<Drill> Bushings
        {
            get { return (AddCollection<Drill>)this["Bushings"]; }
            set { this["Bushings"] = value; }
        }
        /// <summary>
        /// 钻井液密度
        /// </summary>
        [ConfigurationProperty("fluidDensity", IsRequired = true)]
        public decimal FluidDensity
        {
            get { return (decimal)this["fluidDensity"]; }
            set { this["fluidDensity"] = value; }
        }
        /// <summary>
        /// 游车-吊卡总质量
        /// </summary>
        [ConfigurationProperty("elevatorWeight", IsRequired = true)]
        public decimal ElevatorWeight
        {
            get { return (decimal)this["elevatorWeight"]; }
            set { this["elevatorWeight"] = value; }
        }
        /// <summary>
        /// 钻井深度（浅）
        /// </summary>
        [ConfigurationProperty("drillingShallowHeight", IsRequired = true)]
        public decimal DrillingShallowHeight
        {
            get { return (decimal)this["drillingShallowHeight"]; }
            set { this["drillingShallowHeight"] = value; }
        }
        /// <summary>
        /// 钻井深度（深）
        /// </summary>
        [ConfigurationProperty("drillingDeepHeight", IsRequired = true)]
        public decimal DrillingDeepHeight
        {
            get { return (decimal)this["drillingDeepHeight"]; }
            set { this["drillingDeepHeight"] = value; }
        }
        /// <summary>
        /// 钻机驱动方式
        /// </summary>
        [ConfigurationProperty("drillingType", IsRequired = true)]
        public DrillingTypeEnum DrillingType
        {
            get { return (DrillingTypeEnum)this["drillingType"]; }
            set { this["drillingType"] = value; }
        }
        /// <summary>
        /// 划眼次数
        /// </summary>
        [ConfigurationProperty("redressingCount", IsRequired = true)]
        public long RedressingCount
        {
            get { return (long)this["redressingCount"]; }
            set { this["redressingCount"] = value; }
        }
        /// <summary>
        /// 起下钻钻井深度（浅）
        /// </summary>
        [ConfigurationProperty("tripShallowHeight", IsRequired = true)]
        public decimal TripShallowHeight
        {
            get { return (decimal)this["tripShallowHeight"]; }
            set { this["tripShallowHeight"] = value; }
        }
        /// <summary>
        /// 起下钻钻井深度（深）
        /// </summary>
        [ConfigurationProperty("tripDeepHeight", IsRequired = true)]
        public decimal TripDeepHeight
        {
            get { return (decimal)this["tripDeepHeight"]; }
            set { this["tripDeepHeight"] = value; }
        }
        /// <summary>
        /// 起下钻次数
        /// </summary>
        [ConfigurationProperty("tripCount", IsRequired = true)]
        public long TripCount
        {
            get { return (long)this["tripCount"]; }
            set { this["tripCount"] = value; }
        }
        /// <summary>
        /// 下套管深度
        /// </summary>
        [ConfigurationProperty("bushingHeight", IsRequired = true)]
        public decimal BushingHeight
        {
            get { return (decimal)this["bushingHeight"]; }
            set { this["bushingHeight"] = value; }
        }
        /// <summary>
        /// 取岩心钻井深度（浅）
        /// </summary>
        [ConfigurationProperty("coringShallowHeight", IsRequired = true)]
        public decimal CoringShallowHeight
        {
            get { return (decimal)this["coringShallowHeight"]; }
            set { this["coringShallowHeight"] = value; }
        }
        /// <summary>
        /// 取岩心钻井深度（深）
        /// </summary>
        [ConfigurationProperty("coringDeepHeight", IsRequired = true)]
        public decimal CoringDeepHeight
        {
            get { return (decimal)this["coringDeepHeight"]; }
            set { this["coringDeepHeight"] = value; }
        }
    }

    /// <summary>
    /// 钻杆/加重钻杆/钻铤/套管
    /// </summary>
    public class Drill : ConfigurationElement, ISectionCollectionItem
    {
        /// <summary>
        /// 名称
        /// </summary>
        [ConfigurationProperty("name", IsRequired = true)]
        public string Name
        {
            get { return (string)this["name"]; }
            set { this["name"] = value; }
        }
        /// <summary>
        /// 质量
        /// </summary>
        [ConfigurationProperty("weight", IsRequired = true)]
        public decimal Weight
        {
            get { return (decimal)this["weight"]; }
            set { this["weight"] = value; }
        }
        /// <summary>
        /// 长度
        /// </summary>
        [ConfigurationProperty("length", IsRequired = true)]
        public decimal Length
        {
            get { return (decimal)this["length"]; }
            set { this["length"] = value; }
        }

        public object GetKey()
        {
            return this.Name;
        }
    }
}