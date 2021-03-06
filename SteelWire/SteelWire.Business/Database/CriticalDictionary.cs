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
    
    public partial class CriticalDictionary
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CriticalDictionary()
        {
            this.CriticalConfig = new HashSet<CriticalConfig>();
            this.WireropeCutRole = new HashSet<WireropeCutRole>();
            this.WireropeEfficiency = new HashSet<WireropeEfficiency>();
            this.WireropeWorkload = new HashSet<WireropeWorkload>();
        }
    
        public long ID { get; set; }
        public long ConfigUserID { get; set; }
        public System.DateTime ConfigTime { get; set; }
        public long ConfigTimeStamp { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CriticalConfig> CriticalConfig { get; set; }
        public virtual SecurityUser SecurityUser { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WireropeCutRole> WireropeCutRole { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WireropeEfficiency> WireropeEfficiency { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WireropeWorkload> WireropeWorkload { get; set; }
    }
}
