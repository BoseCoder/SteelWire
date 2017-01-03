using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace SteelWire.Business.Database
{
    public abstract class SteelWireBaseContext : DbContext
    {
        protected SteelWireBaseContext(string connectionName)
            : base($"name={connectionName}")
        { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }

        public virtual DbSet<CriticalConfig> CriticalConfig { get; set; }
        public virtual DbSet<CriticalDictionary> CriticalDictionary { get; set; }
        public virtual DbSet<CriticalRecord> CriticalRecord { get; set; }
        public virtual DbSet<CumulationConfig> CumulationConfig { get; set; }
        public virtual DbSet<CumulationDictionary> CumulationDictionary { get; set; }
        public virtual DbSet<CumulationRecord> CumulationRecord { get; set; }
        public virtual DbSet<CutRecord> CutRecord { get; set; }
        public virtual DbSet<DrillDeviceConfig> DrillDeviceConfig { get; set; }
        public virtual DbSet<DrillingType> DrillingType { get; set; }
        public virtual DbSet<Machine> Machine { get; set; }
        public virtual DbSet<SecurityUser> SecurityUser { get; set; }
        public virtual DbSet<WirelineInfo> WirelineInfo { get; set; }
        public virtual DbSet<WireropeCutRole> WireropeCutRole { get; set; }
        public virtual DbSet<WireropeEfficiency> WireropeEfficiency { get; set; }
        public virtual DbSet<WireropeWorkload> WireropeWorkload { get; set; }
    }
}
