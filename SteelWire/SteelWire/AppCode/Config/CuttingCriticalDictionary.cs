using System;
using System.Configuration;
using BaseConfig;

namespace SteelWire.AppCode.Config
{
    /// <summary>
    /// 字典Section
    /// </summary>
    public class CuttingCriticalDictionary : ConfigurationSection
    {
        /// <summary>
        /// 钢丝绳每米工作量集合
        /// </summary>
        [ConfigurationProperty("WireropeWorkloads", IsDefaultCollection = true)]
        public AddCollection<WireropeWorkload> WireropeWorkloads
        {
            get { return (AddCollection<WireropeWorkload>)this["WireropeWorkloads"]; }
            set { this["WireropeWorkloads"] = value; }
        }

        /// <summary>
        /// 钢丝绳切绳规则（井架高度）集合
        /// </summary>
        [ConfigurationProperty("WireropeCutRoles", IsDefaultCollection = true)]
        public AddCollection<WireropeCutRole> WireropeCutRoles
        {
            get { return (AddCollection<WireropeCutRole>)this["WireropeCutRoles"]; }
            set { this["WireropeCutRoles"] = value; }
        }

        /// <summary>
        /// 缠绳效率集合
        /// </summary>
        [ConfigurationProperty("WireropeEfficiencies", IsDefaultCollection = true)]
        public AddCollection<WireropeEfficiency> WireropeEfficiencies
        {
            get { return (AddCollection<WireropeEfficiency>)this["WireropeEfficiencies"]; }
            set { this["WireropeEfficiencies"] = value; }
        }

    }

    /// <summary>
    /// 钢丝绳每米工作量
    /// </summary>
    public class WireropeWorkload : ConfigurationElement, ISectionCollectionItem
    {
        /// <summary>
        /// 钢丝绳直径
        /// </summary>
        [ConfigurationProperty("diameter", IsRequired = true)]
        public decimal Diameter
        {
            get { return (decimal)this["diameter"]; }
            set { this["diameter"] = value; }
        }

        /// <summary>
        /// 每米工作量
        /// </summary>
        [ConfigurationProperty("workload", IsRequired = true)]
        public decimal Workload
        {
            get { return (decimal)this["workload"]; }
            set { this["workload"] = value; }
        }

        public object GetKey()
        {
            return this.Diameter;
        }
    }

    /// <summary>
    /// 钢丝绳切绳规则（井架高度）
    /// </summary>
    public class WireropeCutRole : ConfigurationElement, ISectionCollectionItem
    {
        /// <summary>
        /// ID
        /// </summary>
        [ConfigurationProperty("id", IsRequired = true)]
        public string ID
        {
            get { return (string)this["id"]; }
            set { this["id"] = value; }
        }

        /// <summary>
        /// 井架高度最小值
        /// </summary>
        [ConfigurationProperty("minDerrickHeight", IsRequired = true)]
        public decimal MinDerrickHeight
        {
            get { return (decimal)this["minDerrickHeight"]; }
            set { this["minDerrickHeight"] = value; }
        }

        /// <summary>
        /// 井架高度最大值
        /// </summary>
        [ConfigurationProperty("maxDerrickHeight", IsRequired = true)]
        public decimal MaxDerrickHeight
        {
            get { return (decimal)this["maxDerrickHeight"]; }
            set { this["maxDerrickHeight"] = value; }
        }

        /// <summary>
        /// 切绳长度最小值
        /// </summary>
        [ConfigurationProperty("minCutLength", IsRequired = true)]
        public decimal MinCutLength
        {
            get { return (decimal)this["minCutLength"]; }
            set { this["minCutLength"] = value; }
        }

        /// <summary>
        /// 切绳长度最大值
        /// </summary>
        [ConfigurationProperty("maxCutLength", IsRequired = true)]
        public decimal MaxCutLength
        {
            get { return (decimal)this["maxCutLength"]; }
            set { this["maxCutLength"] = value; }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public WireropeCutRole()
        {
            this.ID = Guid.NewGuid().ToString("N");
        }

        public object GetKey()
        {
            return this.ID;
        }
    }

    /// <summary>
    /// 缠绳效率
    /// </summary>
    public class WireropeEfficiency : ConfigurationElement, ISectionCollectionItem
    {
        /// <summary>
        /// 承载绳根数
        /// </summary>
        [ConfigurationProperty("count", IsRequired = true)]
        public int Count
        {
            get { return (int)this["count"]; }
            set { this["count"] = value; }
        }

        /// <summary>
        /// 滑动轴承滑轮缠绕效率
        /// </summary>
        [ConfigurationProperty("slidingValue", IsRequired = true)]
        public decimal SlidingValue
        {
            get { return (decimal)this["slidingValue"]; }
            set { this["slidingValue"] = value; }
        }

        /// <summary>
        /// 滚动轴承滑轮缠绕效率
        /// </summary>
        [ConfigurationProperty("rollingValue", IsRequired = true)]
        public decimal RollingValue
        {
            get { return (decimal)this["rollingValue"]; }
            set { this["rollingValue"] = value; }
        }

        public object GetKey()
        {
            return this.Count;
        }
    }
}
