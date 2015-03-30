using System.Configuration;

namespace SteelWire.AppCode.Config
{
    public class CuttingCriticalConfig : ConfigurationSection
    {
        [ConfigurationProperty("derrickHeight", IsRequired = true)]
        public decimal DerrickHeight
        {
            get { return (decimal)this["derrickHeight"]; }
            set { this["derrickHeight"] = value; }
        }
        [ConfigurationProperty("wirelineMaxPower", IsRequired = true)]
        public decimal WirelineMaxPower
        {
            get { return (decimal)this["wirelineMaxPower"]; }
            set { this["wirelineMaxPower"] = value; }
        }
        [ConfigurationProperty("rotaryHookWorkload", IsRequired = true)]
        public decimal RotaryHookWorkload
        {
            get { return (decimal)this["rotaryHookWorkload"]; }
            set { this["rotaryHookWorkload"] = value; }
        }
        [ConfigurationProperty("rollerDiameter", IsRequired = true)]
        public decimal RollerDiameter
        {
            get { return (decimal)this["rollerDiameter"]; }
            set { this["rollerDiameter"] = value; }
        }
        [ConfigurationProperty("wirelineDiameter", IsRequired = true)]
        public decimal WirelineDiameter
        {
            get { return (decimal)this["wirelineDiameter"]; }
            set { this["wirelineDiameter"] = value; }
        }
        [ConfigurationProperty("ropeCount", IsRequired = true)]
        public int RopeCount
        {
            get { return (int)this["ropeCount"]; }
            set { this["ropeCount"] = value; }
        }
    }
}