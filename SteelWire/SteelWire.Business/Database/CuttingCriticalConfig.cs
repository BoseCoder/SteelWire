//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace SteelWire.Business.Database
{
    using System;
    using System.Collections.Generic;
    
    public partial class CuttingCriticalConfig
    {
        public int ID { get; set; }
        public int DictionaryID { get; set; }
        public int ConfigUserID { get; set; }
        public decimal DerrickHeight { get; set; }
        public decimal WirelineMaxPower { get; set; }
        public decimal RotaryHookWorkload { get; set; }
        public decimal RollerDiameter { get; set; }
        public decimal WirelineDiameter { get; set; }
        public int RopeCount { get; set; }
        public System.DateTime ConfigTime { get; set; }
        public decimal CuttingCriticalValue { get; set; }
        public long ConfigTimeStamp { get; set; }
    
        public virtual CuttingCriticalDictionary CuttingCriticalDictionary { get; set; }
        public virtual SecurityUser SecurityUser { get; set; }
    }
}
