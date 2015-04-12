using System;
using System.Collections.Generic;
using System.Linq;
using SteelWire.Lang;

namespace SteelWire.AppCode.Dependencies
{
    public class DependencyDrillPipeCollection
    {
        public event EventHandler ItemsChangedHandler;
        public DependencyCollection<List<DrillPipeConfig>, DrillPipeConfig> DrillPipes { get; private set; }
        public DependencyCollection<List<HeavierDrillPipeConfig>, HeavierDrillPipeConfig> HeavierDrillPipes { get; private set; }
        public DependencyItem<decimal> AverageWeight { get; private set; }
        public DependencyItem<decimal> TotalLength { get; private set; }
        public DependencyDrillPipeCollection()
        {
            DrillPipes = new DependencyCollection<List<DrillPipeConfig>, DrillPipeConfig>();
            DrillPipes.SetValue(DependencyCollection<List<DrillPipeConfig>, DrillPipeConfig>.ItemsPropertyKey, new List<DrillPipeConfig>());
            HeavierDrillPipes = new DependencyCollection<List<HeavierDrillPipeConfig>, HeavierDrillPipeConfig>();
            HeavierDrillPipes.SetValue(DependencyCollection<List<HeavierDrillPipeConfig>, HeavierDrillPipeConfig>.ItemsPropertyKey, new List<HeavierDrillPipeConfig>());
            this.AverageWeight = new DependencyItem<decimal>();
            this.TotalLength = new DependencyItem<decimal>();
        }
        protected void OnItemsChanged(EventArgs e)
        {
            var drillPipes = this.DrillPipes.Items.Where(d => d.Weight.ItemValue > 0 && d.Length.ItemValue > 0 && d.StandLength.ItemValue > 0);
            var heavierDrillPipes = this.HeavierDrillPipes.Items.Where(d => d.Weight.ItemValue > 0 && d.Length.ItemValue > 0);
            decimal totalLength = drillPipes.Sum(item => item.Length.ItemValue) + heavierDrillPipes.Sum(item => item.Length.ItemValue);
            if (totalLength > 0)
            {
                decimal totalWeight = drillPipes.Sum(item => item.Weight.ItemValue * item.Length.ItemValue)
                    + heavierDrillPipes.Sum(item => item.Weight.ItemValue * item.Length.ItemValue);
                this.AverageWeight.ItemValue = totalWeight / totalLength;
            }
            else
            {
                this.AverageWeight.ItemValue = 0;
            }
            this.TotalLength.ItemValue = drillPipes.Sum(item => item.StandLength.ItemValue);
            EventHandler handler = ItemsChangedHandler;
            if (handler != null)
            {
                handler.Invoke(this, e);
            }
        }
        public void AddRange(IEnumerable<DrillPipeConfig> items)
        {
            foreach (DrillPipeConfig item in items)
            {
                Add(item);
            }
        }
        public void Add(DrillPipeConfig item)
        {
            item.LocalName = new DependencyItem<string>
            {
                ItemValue = LanguageManager.GetLocalResourceStringRight(item.NameKey, item.Name.ItemValue)
            };
            item.Name.ItemValueChangedHandler += NameChanged;
            item.Weight.ItemValueChangedHandler += DrillPropertyChanged;
            item.Length.ItemValueChangedHandler += DrillPropertyChanged;
            item.StandLength.ItemValueChangedHandler += DrillPropertyChanged;
            this.DrillPipes.Add(item);
        }
        public void AddRange(IEnumerable<HeavierDrillPipeConfig> items)
        {
            foreach (HeavierDrillPipeConfig item in items)
            {
                Add(item);
            }
        }
        public void Add(HeavierDrillPipeConfig item)
        {
            item.LocalName = new DependencyItem<string>
            {
                ItemValue = LanguageManager.GetLocalResourceStringRight(item.NameKey, item.Name.ItemValue)
            };
            item.Name.ItemValueChangedHandler += NameChanged;
            item.Weight.ItemValueChangedHandler += DrillPropertyChanged;
            item.Length.ItemValueChangedHandler += DrillPropertyChanged;
            this.HeavierDrillPipes.Add(item);
        }
        private void NameChanged(object sender, EventArgs e)
        {
            DrillConfig item = (DrillConfig)sender;
            item.LocalName.ItemValue = LanguageManager.GetLocalResourceStringRight(item.NameKey, item.Name.ItemValue);
        }
        private void DrillPropertyChanged(object sender, EventArgs e)
        {
            OnItemsChanged(e);
        }
    }

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
            item.Weight.ItemValueChangedHandler += DrillPropertyChanged;
            item.Length.ItemValueChangedHandler += DrillPropertyChanged;
            base.Add(item);
        }
        private void NameChanged(object sender, EventArgs e)
        {
            T item = (T)sender;
            item.LocalName.ItemValue = LanguageManager.GetLocalResourceStringRight(item.NameKey, item.Name.ItemValue);
        }
        private void DrillPropertyChanged(object sender, EventArgs e)
        {
            OnItemsChanged(e);
        }
    }

    public class HeavierDrillPipeConfig : DrillConfig
    {
        public static DependencyItem<string> StaticWeightTitle { get; private set; }
        public static DependencyItem<string> StaticLengthTitle { get; private set; }
        static HeavierDrillPipeConfig()
        {
            StaticWeightTitle = new DependencyItem<string>
            {
                ItemValue = LanguageManager.GetLocalResourceStringLeft("DrillPipeWeight")
            };
            StaticLengthTitle = new DependencyItem<string>
            {
                ItemValue = LanguageManager.GetLocalResourceStringLeft("DrillPipeLength")
            };
        }

        public override string NameKey
        {
            get { return "HeavierDrillPipeName"; }
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

    public class DrillPipeConfig : DrillConfig
    {
        public static DependencyItem<string> StaticWeightTitle { get; private set; }
        public static DependencyItem<string> StaticLengthTitle { get; private set; }
        public static DependencyItem<string> StaticStandLengthTitle { get; private set; }
        static DrillPipeConfig()
        {
            StaticWeightTitle = new DependencyItem<string>
            {
                ItemValue = LanguageManager.GetLocalResourceStringLeft("DrillPipeWeight")
            };
            StaticLengthTitle = new DependencyItem<string>
            {
                ItemValue = LanguageManager.GetLocalResourceStringLeft("DrillPipeLength")
            };
            StaticStandLengthTitle = new DependencyItem<string>
            {
                ItemValue = LanguageManager.GetLocalResourceStringLeft("DrillPipeStandLength")
            };
        }

        public string StandLengthTitle
        {
            get { return StaticStandLengthTitle.ItemValue; }
        }
        public DependencyItem<decimal> StandLength { get; set; }

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
            StaticWeightTitle = new DependencyItem<string>
            {
                ItemValue = LanguageManager.GetLocalResourceStringLeft("DrillCollarWeight")
            };
            StaticLengthTitle = new DependencyItem<string>
            {
                ItemValue = LanguageManager.GetLocalResourceStringLeft("DrillCollarLength")
            };
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
