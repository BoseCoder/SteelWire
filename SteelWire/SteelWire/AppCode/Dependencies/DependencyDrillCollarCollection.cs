using System;
using System.Collections.Generic;
using System.Linq;
using SteelWire.Lang;

namespace SteelWire.AppCode.Dependencies
{
    public class DependencyDrillCollection<T> : DependencyCollection<List<T>, T>
        where T : DrillConfig, new()
    {
        public DependencyItem<decimal> AverageWeight { get; private set; }
        public DependencyItem<decimal> TotalLength { get; private set; }
        public DependencyDrillCollection()
        {
            this.AverageWeight = new DependencyItem<decimal>();
            this.TotalLength = new DependencyItem<decimal>();
            SetValue(ItemsPropertyKey, new List<T>());
        }
        protected override void OnItemsChanged(EventArgs e)
        {
            var list = this.Items.Where(d => d.Weight.ItemValue > 0 && d.Length.ItemValue > 0);
            this.TotalLength.ItemValue = list.Sum(item => item.Length.ItemValue);
            if (this.TotalLength.ItemValue > 0)
            {
                decimal totalWeight = list.Sum(item => item.Weight.ItemValue * item.Length.ItemValue);
                this.AverageWeight.ItemValue = totalWeight / this.TotalLength.ItemValue;
            }
            else
            {
                this.AverageWeight.ItemValue = 0;
            }
            base.OnItemsChanged(e);
        }
        public override void Add(T item)
        {
            item.LocalName = new DependencyItem<string>
            {
                ItemValue = LanguageManager.GetLocalResourceStringRight(item.NameKey, item.Name.ItemValue)
            };
            item.Name.ItemValueChangedHandler += NameChanged;
            item.Weight.ItemValueChangedHandler += DrillCollarChanged;
            item.Length.ItemValueChangedHandler += DrillCollarChanged;
            base.Add(item);
        }
        private void NameChanged(object sender, EventArgs e)
        {
            T item = (T) sender;
            item.LocalName.ItemValue = LanguageManager.GetLocalResourceStringRight(item.NameKey, item.Name.ItemValue);
        }
        private void DrillCollarChanged(object sender, EventArgs e)
        {
            OnItemsChanged(e);
        }
    }

    public class DrillPipeConfig : DrillConfig
    {
        public static DependencyItem<string> StaticWeightTitle { get; private set; }
        public static DependencyItem<string> StaticLengthTitle { get; private set; }
        static DrillPipeConfig()
        {
            StaticWeightTitle = new DependencyItem<string>();
            StaticLengthTitle = new DependencyItem<string>();
        }

        public override string NameKey
        {
            get { return "DrillPipeName"; }
        }

        protected override string GetWeightTitle()
        {
            return StaticWeightTitle.ItemValue;
        }

        protected override string GetLengthTitle()
        {
            return StaticLengthTitle.ItemValue;
        }
    }

    public class DrillCollarConfig : DrillConfig
    {
        public static DependencyItem<string> StaticWeightTitle { get; private set; }
        public static DependencyItem<string> StaticLengthTitle { get; private set; }
        static DrillCollarConfig()
        {
            StaticWeightTitle = new DependencyItem<string>();
            StaticLengthTitle = new DependencyItem<string>();
        }

        public override string NameKey
        {
            get { return "DrillCollarName"; }
        }

        protected override string GetWeightTitle()
        {
            return StaticWeightTitle.ItemValue;
        }

        protected override string GetLengthTitle()
        {
            return StaticLengthTitle.ItemValue;
        }
    }

    public abstract class DrillConfig
    {
        public abstract string NameKey { get; }
        public DependencyItem<string> Name { get; set; }
        public DependencyItem<string> LocalName { get; set; }
        public string WeightTitle
        {
            get { return GetWeightTitle(); }
        }
        public DependencyItem<decimal> Weight { get; set; }
        public string LengthTitle
        {
            get { return GetLengthTitle(); }
        }
        public DependencyItem<decimal> Length { get; set; }

        protected abstract string GetWeightTitle();
        protected abstract string GetLengthTitle();
    }
}
