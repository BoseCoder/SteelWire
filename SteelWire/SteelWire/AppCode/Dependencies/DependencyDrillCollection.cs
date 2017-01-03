using System;
using System.Collections.Generic;
using System.Linq;

namespace SteelWire.AppCode.Dependencies
{
    public class DependencyDrillCollection<T> : DependencyCollection<T>
        where T : DependencyDrillConfig
    {
        public decimal AverageWeight { get; private set; }
        public decimal TotalLength { get; private set; }
        protected override void OnItemsChanged(EventArgs e)
        {
            List<DependencyObject<T>> list = this.Items.Where(d => d.Value.Weight.Value > 0 && d.Value.Length.Value > 0).ToList();
            this.TotalLength = list.Sum(item => item.Value.Length.Value);
            if (this.TotalLength > 0)
            {
                if (list.Count == 1)
                {
                    this.AverageWeight = list.First().Value.Weight.Value;
                }
                else
                {
                    decimal totalWeight = list.Sum(item => item.Value.Weight.Value * item.Value.Length.Value);
                    this.AverageWeight = totalWeight / this.TotalLength;
                }
            }
            else
            {
                this.AverageWeight = 0;
            }
            base.OnItemsChanged(e);
        }
        public override void Add(T item)
        {
            item.Weight.ValueChangedHandler += DrillPropertyChanged;
            item.Length.ValueChangedHandler += DrillPropertyChanged;
            base.Add(item);
        }
        private void DrillPropertyChanged(object sender, EventArgs e)
        {
            OnItemsChanged(e);
        }
    }
}
