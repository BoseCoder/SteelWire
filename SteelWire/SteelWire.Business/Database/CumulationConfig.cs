//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace SteelWire.Business.Database
{
    using System;
    using System.Collections.Generic;
    
    public partial class CumulationConfig
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CumulationConfig()
        {
            this.CumulationRecord = new HashSet<CumulationRecord>();
            this.DrillDeviceConfig = new HashSet<DrillDeviceConfig>();
        }
    
        public long ID { get; set; }
        public long DictionaryID { get; set; }
        public long ConfigUserID { get; set; }
        public decimal RealRotaryHookWorkload { get; set; }
        public decimal FluidDensity { get; set; }
        public decimal ElevatorWeight { get; set; }
        public decimal DrillingShallowHeight { get; set; }
        public decimal DrillingDeepHeight { get; set; }
        public string DrillingType { get; set; }
        public long RedressingCount { get; set; }
        public decimal TripShallowHeight { get; set; }
        public decimal TripDeepHeight { get; set; }
        public long TripCount { get; set; }
        public decimal BushingHeight { get; set; }
        public decimal CoringShallowHeight { get; set; }
        public decimal CoringDeepHeight { get; set; }
        public System.DateTime ConfigTime { get; set; }
        public long ConfigTimeStamp { get; set; }
    
        public virtual SecurityUser SecurityUser { get; set; }
        public virtual CumulationDictionary CumulationDictionary { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CumulationRecord> CumulationRecord { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DrillDeviceConfig> DrillDeviceConfig { get; set; }
    }
}