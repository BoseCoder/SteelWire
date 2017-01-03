using System.Configuration;
using BaseConfig;

namespace SteelWire.AppCode.Config
{
    public class WorkDictionary : ConfigurationSection
    {
        /// <summary>
        /// 钻机驱动方式集合
        /// </summary>
        [ConfigurationProperty("DrillingTypes", IsDefaultCollection = true)]
        public AddCollection<DrillingType> DrillingTypes
        {
            get { return (AddCollection<DrillingType>)this["DrillingTypes"]; }
            set { this["DrillingTypes"] = value; }
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
        public DrillingTypeEnum Name
        {
            get { return (DrillingTypeEnum)this["name"]; }
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
}