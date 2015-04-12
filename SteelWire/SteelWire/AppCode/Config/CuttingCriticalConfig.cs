using System.Configuration;

namespace SteelWire.AppCode.Config
{
    /// <summary>
    /// �����ٽ�ֵ��ǰ����
    /// </summary>
    public class CuttingCriticalConfig : ConfigurationSection
    {
        /// <summary>
        /// ���ܸ߶�
        /// </summary>
        [ConfigurationProperty("derrickHeight", IsRequired = true)]
        public decimal DerrickHeight
        {
            get { return (decimal)this["derrickHeight"]; }
            set { this["derrickHeight"] = value; }
        }
        /// <summary>
        /// ��˿���ƶ�����
        /// </summary>
        [ConfigurationProperty("wirelineMaxPower", IsRequired = true)]
        public decimal WirelineMaxPower
        {
            get { return (decimal)this["wirelineMaxPower"]; }
            set { this["wirelineMaxPower"] = value; }
        }
        /// <summary>
        /// ���غ�
        /// </summary>
        [ConfigurationProperty("rotaryHookWorkload", IsRequired = true)]
        public decimal RotaryHookWorkload
        {
            get { return (decimal)this["rotaryHookWorkload"]; }
            set { this["rotaryHookWorkload"] = value; }
        }
        /// <summary>
        /// ��Ͳֱ��
        /// </summary>
        [ConfigurationProperty("rollerDiameter", IsRequired = true)]
        public decimal RollerDiameter
        {
            get { return (decimal)this["rollerDiameter"]; }
            set { this["rollerDiameter"] = value; }
        }
        /// <summary>
        /// ��˿��ֱ��
        /// </summary>
        [ConfigurationProperty("wirelineDiameter", IsRequired = true)]
        public decimal WirelineDiameter
        {
            get { return (decimal)this["wirelineDiameter"]; }
            set { this["wirelineDiameter"] = value; }
        }
        /// <summary>
        /// ����������
        /// </summary>
        [ConfigurationProperty("ropeCount", IsRequired = true)]
        public int RopeCount
        {
            get { return (int)this["ropeCount"]; }
            set { this["ropeCount"] = value; }
        }
    }
}