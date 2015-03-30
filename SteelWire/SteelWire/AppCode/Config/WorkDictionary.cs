using System.Configuration;
using BaseConfig;

namespace SteelWire.AppCode.Config
{
    public class WorkDictionary: ConfigurationSection
    {
        /// <summary>
        /// 钻井方式集合
        /// </summary>
        [ConfigurationProperty("DrillingTypes", IsDefaultCollection = true)]
        public AddCollection<DrillingType> DrillingTypes
        {
            get { return (AddCollection<DrillingType>)this["DrillingTypes"]; }
            set { this["DrillingTypes"] = value; }
        }

        /// <summary>
        /// 钻井难度集合
        /// </summary>
        [ConfigurationProperty("DrillingDifficulties", IsDefaultCollection = true)]
        public AddCollection<DrillingDifficulty> DrillingDifficulties
        {
            get { return (AddCollection<DrillingDifficulty>)this["DrillingDifficulties"]; }
            set { this["DrillingDifficulties"] = value; }
        }
    }

    /// <summary>
    /// 钻井方式
    /// </summary>
    public class DrillingType : ConfigurationElement, ISectionCollectionItem
    {
        /// <summary>
        /// 钻井方式名称
        /// </summary>
        [ConfigurationProperty("name", IsRequired = true)]
        public string Name
        {
            get { return string.Format("{0}", this["name"]); }
            set { this["name"] = value; }
        }

        /// <summary>
        /// 钻井方式系数
        /// </summary>
        [ConfigurationProperty("coefficient", IsRequired = true)]
        public decimal Coefficient
        {
            get { return (decimal)this["coefficient"]; }
            set { this["coefficient"] = value; }
        }

        public object GetKey()
        {
            return this.Name;
        }
    }

    /// <summary>
    /// 钻井难度
    /// </summary>
    public class DrillingDifficulty : ConfigurationElement, ISectionCollectionItem
    {
        /// <summary>
        /// 钻井难度名称
        /// </summary>
        [ConfigurationProperty("name", IsRequired = true)]
        public string Name
        {
            get { return string.Format("{0}", this["name"]); }
            set { this["name"] = value; }
        }

        /// <summary>
        /// 钻井难度
        /// </summary>
        [ConfigurationProperty("difficulty", IsRequired = true)]
        public decimal Difficulty
        {
            get { return (decimal)this["difficulty"]; }
            set { this["difficulty"] = value; }
        }

        public object GetKey()
        {
            return this.Name;
        }
    }
}