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
    
    public partial class CumulationRecord
    {
        public long ID { get; set; }
        public long CutRecordID { get; set; }
        public long CriticalConfigID { get; set; }
        public long CumulationConfigID { get; set; }
        public long CalculateUserID { get; set; }
        public decimal CumulationValue { get; set; }
        public System.DateTime CalculateTime { get; set; }
    
        public virtual CriticalConfig CriticalConfig { get; set; }
        public virtual CumulationConfig CumulationConfig { get; set; }
        public virtual CutRecord CutRecord { get; set; }
        public virtual SecurityUser SecurityUser { get; set; }
    }
}
