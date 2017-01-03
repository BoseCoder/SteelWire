using System.Configuration;
using BaseConfig;

namespace SteelWire.AppCode.Config
{
    /// <summary>
    /// ��˿���ֹ�����㵱ǰ����
    /// </summary>
    public class WorkConfig : ConfigurationSection
    {
        /// <summary>
        /// �������ʾ����
        /// </summary>
        [ConfigurationProperty("realRotaryHookWorkload", IsRequired = true)]
        public decimal RealRotaryHookWorkload
        {
            get { return (decimal)this["realRotaryHookWorkload"]; }
            set { this["realRotaryHookWorkload"] = value; }
        }
        /// <summary>
        /// ���
        /// </summary>
        [ConfigurationProperty("DrillPipes", IsRequired = true)]
        public AddCollection<Drill> DrillPipes
        {
            get { return (AddCollection<Drill>)this["DrillPipes"]; }
            set { this["DrillPipes"] = value; }
        }
        /// <summary>
        /// �������
        /// </summary>
        [ConfigurationProperty("HeavierDrillPipes", IsRequired = true)]
        public AddCollection<Drill> HeavierDrillPipes
        {
            get { return (AddCollection<Drill>)this["HeavierDrillPipes"]; }
            set { this["HeavierDrillPipes"] = value; }
        }
        /// <summary>
        /// ����
        /// </summary>
        [ConfigurationProperty("DrillCollars", IsRequired = true)]
        public AddCollection<Drill> DrillCollars
        {
            get { return (AddCollection<Drill>)this["DrillCollars"]; }
            set { this["DrillCollars"] = value; }
        }
        /// <summary>
        /// �׹�
        /// </summary>
        [ConfigurationProperty("Bushings", IsRequired = true)]
        public AddCollection<Drill> Bushings
        {
            get { return (AddCollection<Drill>)this["Bushings"]; }
            set { this["Bushings"] = value; }
        }
        /// <summary>
        /// �꾮Һ�ܶ�
        /// </summary>
        [ConfigurationProperty("fluidDensity", IsRequired = true)]
        public decimal FluidDensity
        {
            get { return (decimal)this["fluidDensity"]; }
            set { this["fluidDensity"] = value; }
        }
        /// <summary>
        /// �γ�-����������
        /// </summary>
        [ConfigurationProperty("elevatorWeight", IsRequired = true)]
        public decimal ElevatorWeight
        {
            get { return (decimal)this["elevatorWeight"]; }
            set { this["elevatorWeight"] = value; }
        }
        /// <summary>
        /// �꾮��ȣ�ǳ��
        /// </summary>
        [ConfigurationProperty("drillingShallowHeight", IsRequired = true)]
        public decimal DrillingShallowHeight
        {
            get { return (decimal)this["drillingShallowHeight"]; }
            set { this["drillingShallowHeight"] = value; }
        }
        /// <summary>
        /// �꾮��ȣ��
        /// </summary>
        [ConfigurationProperty("drillingDeepHeight", IsRequired = true)]
        public decimal DrillingDeepHeight
        {
            get { return (decimal)this["drillingDeepHeight"]; }
            set { this["drillingDeepHeight"] = value; }
        }
        /// <summary>
        /// ���������ʽ
        /// </summary>
        [ConfigurationProperty("drillingType", IsRequired = true)]
        public DrillingTypeEnum DrillingType
        {
            get { return (DrillingTypeEnum)this["drillingType"]; }
            set { this["drillingType"] = value; }
        }
        /// <summary>
        /// ���۴���
        /// </summary>
        [ConfigurationProperty("redressingCount", IsRequired = true)]
        public long RedressingCount
        {
            get { return (long)this["redressingCount"]; }
            set { this["redressingCount"] = value; }
        }
        /// <summary>
        /// �������꾮��ȣ�ǳ��
        /// </summary>
        [ConfigurationProperty("tripShallowHeight", IsRequired = true)]
        public decimal TripShallowHeight
        {
            get { return (decimal)this["tripShallowHeight"]; }
            set { this["tripShallowHeight"] = value; }
        }
        /// <summary>
        /// �������꾮��ȣ��
        /// </summary>
        [ConfigurationProperty("tripDeepHeight", IsRequired = true)]
        public decimal TripDeepHeight
        {
            get { return (decimal)this["tripDeepHeight"]; }
            set { this["tripDeepHeight"] = value; }
        }
        /// <summary>
        /// ���������
        /// </summary>
        [ConfigurationProperty("tripCount", IsRequired = true)]
        public long TripCount
        {
            get { return (long)this["tripCount"]; }
            set { this["tripCount"] = value; }
        }
        /// <summary>
        /// ���׹����
        /// </summary>
        [ConfigurationProperty("bushingHeight", IsRequired = true)]
        public decimal BushingHeight
        {
            get { return (decimal)this["bushingHeight"]; }
            set { this["bushingHeight"] = value; }
        }
        /// <summary>
        /// ȡ�����꾮��ȣ�ǳ��
        /// </summary>
        [ConfigurationProperty("coringShallowHeight", IsRequired = true)]
        public decimal CoringShallowHeight
        {
            get { return (decimal)this["coringShallowHeight"]; }
            set { this["coringShallowHeight"] = value; }
        }
        /// <summary>
        /// ȡ�����꾮��ȣ��
        /// </summary>
        [ConfigurationProperty("coringDeepHeight", IsRequired = true)]
        public decimal CoringDeepHeight
        {
            get { return (decimal)this["coringDeepHeight"]; }
            set { this["coringDeepHeight"] = value; }
        }
    }

    /// <summary>
    /// ���/�������/����/�׹�
    /// </summary>
    public class Drill : ConfigurationElement, ISectionCollectionItem
    {
        /// <summary>
        /// ����
        /// </summary>
        [ConfigurationProperty("name", IsRequired = true)]
        public string Name
        {
            get { return (string)this["name"]; }
            set { this["name"] = value; }
        }
        /// <summary>
        /// ����
        /// </summary>
        [ConfigurationProperty("weight", IsRequired = true)]
        public decimal Weight
        {
            get { return (decimal)this["weight"]; }
            set { this["weight"] = value; }
        }
        /// <summary>
        /// ����
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