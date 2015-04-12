using System.Configuration;

namespace SteelWire.AppCode.Config
{
    /// <summary>
    /// ÇÐÉþÁÙ½çÖµµ±Ç°ÅäÖÃ
    /// </summary>
    public class CuttingCriticalConfig : ConfigurationSection
    {
        /// <summary>
        /// ¾®¼Ü¸ß¶È
        /// </summary>
        [ConfigurationProperty("derrickHeight", IsRequired = true)]
        public decimal DerrickHeight
        {
            get { return (decimal)this["derrickHeight"]; }
            set { this["derrickHeight"] = value; }
        }
        /// <summary>
        /// ¸ÖË¿ÉþÆÆ¶ÏÀ­Á¦
        /// </summary>
        [ConfigurationProperty("wirelineMaxPower", IsRequired = true)]
        public decimal WirelineMaxPower
        {
            get { return (decimal)this["wirelineMaxPower"]; }
            set { this["wirelineMaxPower"] = value; }
        }
        /// <summary>
        /// ´ó¹³ÔØºÉ
        /// </summary>
        [ConfigurationProperty("rotaryHookWorkload", IsRequired = true)]
        public decimal RotaryHookWorkload
        {
            get { return (decimal)this["rotaryHookWorkload"]; }
            set { this["rotaryHookWorkload"] = value; }
        }
        /// <summary>
        /// ¹öÍ²Ö±¾¶
        /// </summary>
        [ConfigurationProperty("rollerDiameter", IsRequired = true)]
        public decimal RollerDiameter
        {
            get { return (decimal)this["rollerDiameter"]; }
            set { this["rollerDiameter"] = value; }
        }
        /// <summary>
        /// ¸ÖË¿ÉþÖ±¾¶
        /// </summary>
        [ConfigurationProperty("wirelineDiameter", IsRequired = true)]
        public decimal WirelineDiameter
        {
            get { return (decimal)this["wirelineDiameter"]; }
            set { this["wirelineDiameter"] = value; }
        }
        /// <summary>
        /// ³ÐÔØÉþ¸ùÊý
        /// </summary>
        [ConfigurationProperty("ropeCount", IsRequired = true)]
        public int RopeCount
        {
            get { return (int)this["ropeCount"]; }
            set { this["ropeCount"] = value; }
        }
    }
}