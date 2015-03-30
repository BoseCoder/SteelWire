using System.Configuration;
using BaseConfig;

namespace SteelWire.AppCode.Config
{
    public class WorkConfig : ConfigurationSection
    {
        [ConfigurationProperty("DrillPipes", IsRequired = true)]
        public AddCollection<Drill> DrillPipes
        {
            get { return (AddCollection<Drill>)this["DrillPipes"]; }
            set { this["DrillPipes"] = value; }
        }
        [ConfigurationProperty("DrillCollars", IsRequired = true)]
        public AddCollection<Drill> DrillCollars
        {
            get { return (AddCollection<Drill>)this["DrillCollars"]; }
            set { this["DrillCollars"] = value; }
        }

        [ConfigurationProperty("fluidDensity", IsRequired = true)]
        public decimal FluidDensity
        {
            get { return (decimal)this["fluidDensity"]; }
            set { this["fluidDensity"] = value; }
        }
        [ConfigurationProperty("elevatorWeight", IsRequired = true)]
        public decimal ElevatorWeight
        {
            get { return (decimal)this["elevatorWeight"]; }
            set { this["elevatorWeight"] = value; }
        }

        [ConfigurationProperty("drillingShallowHeight", IsRequired = true)]
        public decimal DrillingShallowHeight
        {
            get { return (decimal)this["drillingShallowHeight"]; }
            set { this["drillingShallowHeight"] = value; }
        }
        [ConfigurationProperty("drillingDeepHeight", IsRequired = true)]
        public decimal DrillingDeepHeight
        {
            get { return (decimal)this["drillingDeepHeight"]; }
            set { this["drillingDeepHeight"] = value; }
        }
        [ConfigurationProperty("drillingType", IsRequired = true)]
        public string DrillingType
        {
            get { return (string)this["drillingType"]; }
            set { this["drillingType"] = value; }
        }
        [ConfigurationProperty("drillingDifficulty", IsRequired = true)]
        public string DrillingDifficulty
        {
            get { return (string)this["drillingDifficulty"]; }
            set { this["drillingDifficulty"] = value; }
        }

        [ConfigurationProperty("tripShallowHeight", IsRequired = true)]
        public decimal TripShallowHeight
        {
            get { return (decimal)this["tripShallowHeight"]; }
            set { this["tripShallowHeight"] = value; }
        }
        [ConfigurationProperty("tripDeepHeight", IsRequired = true)]
        public decimal TripDeepHeight
        {
            get { return (decimal)this["tripDeepHeight"]; }
            set { this["tripDeepHeight"] = value; }
        }
        [ConfigurationProperty("tripCount", IsRequired = true)]
        public int TripCount
        {
            get { return (int)this["tripCount"]; }
            set { this["tripCount"] = value; }
        }

        [ConfigurationProperty("bushingWeight", IsRequired = true)]
        public decimal BushingWeight
        {
            get { return (decimal)this["bushingWeight"]; }
            set { this["bushingWeight"] = value; }
        }
        [ConfigurationProperty("bushingLength", IsRequired = true)]
        public decimal BushingLength
        {
            get { return (decimal)this["bushingLength"]; }
            set { this["bushingLength"] = value; }
        }
        [ConfigurationProperty("bushingHeight", IsRequired = true)]
        public decimal BushingHeight
        {
            get { return (decimal)this["bushingHeight"]; }
            set { this["bushingHeight"] = value; }
        }

        [ConfigurationProperty("coringShallowHeight", IsRequired = true)]
        public decimal CoringShallowHeight
        {
            get { return (decimal)this["coringShallowHeight"]; }
            set { this["coringShallowHeight"] = value; }
        }
        [ConfigurationProperty("coringDeepHeight", IsRequired = true)]
        public decimal CoringDeepHeight
        {
            get { return (decimal)this["coringDeepHeight"]; }
            set { this["coringDeepHeight"] = value; }
        }
    }

    public class Drill : ConfigurationElement, ISectionCollectionItem
    {
        [ConfigurationProperty("name", IsRequired = true)]
        public string Name
        {
            get { return (string)this["name"]; }
            set { this["name"] = value; }
        }

        [ConfigurationProperty("weight", IsRequired = true)]
        public decimal Weight
        {
            get { return (decimal)this["weight"]; }
            set { this["weight"] = value; }
        }

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