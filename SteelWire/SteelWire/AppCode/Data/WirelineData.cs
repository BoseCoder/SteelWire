using SteelWire.AppCode.Dependencies;
using SteelWire.Business.Config;

namespace SteelWire.AppCode.Data
{
    public class WirelineData
    {
        public long Id { get; set; }
        public string Number { get; set; }
        public DependencyObject<string> Diameter { get; } = new DependencyObject<string>();
        public DependencyObject<string> Struct { get; } = new DependencyObject<string>();
        public DependencyObject<string> StrongLevel { get; } = new DependencyObject<string>();
        public DependencyObject<string> TwistDirection { get; } = new DependencyObject<string>();
        public DependencyObject<decimal> OrderLength { get; } = new DependencyObject<decimal>();
        public DependencyObject<UnitSystemEnum> UnitSystem { get; } = new DependencyObject<UnitSystemEnum>();
        public DependencyObject<decimal> CriticalValue { get; } = new DependencyObject<decimal>();
        public DependencyObject<decimal> CumulationValue { get; } = new DependencyObject<decimal>();
        public DependencyObject<decimal> TotalCuttedValue { get; } = new DependencyObject<decimal>();
    }
}
