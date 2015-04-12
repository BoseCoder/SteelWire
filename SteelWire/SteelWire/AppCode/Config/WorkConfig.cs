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
        /// ���
        /// </summary>
        [ConfigurationProperty("DrillPipes", IsRequired = true)]
        public AddCollection<DrillPipe> DrillPipes
        {
            get { return (AddCollection<DrillPipe>)this["DrillPipes"]; }
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
        /// �꾮��ʽ
        /// </summary>
        [ConfigurationProperty("drillingType", IsRequired = true)]
        public string DrillingType
        {
            get { return (string)this["drillingType"]; }
            set { this["drillingType"] = value; }
        }
        /// <summary>
        /// �꾮�Ѷ�
        /// </summary>
        [ConfigurationProperty("drillingDifficulty", IsRequired = true)]
        public string DrillingDifficulty
        {
            get { return (string)this["drillingDifficulty"]; }
            set { this["drillingDifficulty"] = value; }
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
        public decimal TripCount
        {
            get { return (decimal)this["tripCount"]; }
            set { this["tripCount"] = value; }
        }
        /// <summary>
        /// �׹ܹ�������
        /// </summary>
        [ConfigurationProperty("bushingWeight", IsRequired = true)]
        public decimal BushingWeight
        {
            get { return (decimal)this["bushingWeight"]; }
            set { this["bushingWeight"] = value; }
        }
        /// <summary>
        /// �׹ܵ�������
        /// </summary>
        [ConfigurationProperty("bushingLength", IsRequired = true)]
        public decimal BushingLength
        {
            get { return (decimal)this["bushingLength"]; }
            set { this["bushingLength"] = value; }
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
    /// ���
    /// </summary>
    public class DrillPipe : Drill
    {
        /// <summary>
        /// ����
        /// </summary>
        [ConfigurationProperty("standLength", IsRequired = true)]
        public decimal StandLength
        {
            get { return (decimal)this["standLength"]; }
            set { this["standLength"] = value; }
        }
    }

    /// <summary>
    /// ��˺�����
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