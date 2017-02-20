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

        private void CalculateProperty()
        {
            List<DependencyObject<T>> list = this.Items
                .Where(d => d.Value.Weight.Value > decimal.Zero && d.Value.Length.Value > decimal.Zero).ToList();
            this.TotalLength = list.Sum(item => item.Value.Length.Value);
            if (this.TotalLength > decimal.Zero)
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
                this.AverageWeight = decimal.Zero;
            }
        }
        protected override void OnItemsChanged(EventArgs e)
        {
            CalculateProperty();
            base.OnItemsChanged(e);
        }
        protected override void AddItem(T item)
        {
            item.Weight.ValueChangedHandler += DrillPropertyChanged;
            item.Length.ValueChangedHandler += DrillPropertyChanged;
            base.AddItem(item);
        }
        private void DrillPropertyChanged(object sender, EventArgs e)
        {
            CalculateProperty();
            this.OnItemsChangedHandler(e); ;
        }
    }
}
