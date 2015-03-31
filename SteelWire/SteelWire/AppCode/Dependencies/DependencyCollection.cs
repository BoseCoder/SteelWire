using System;
using System.Collections.Generic;
using System.Windows;

namespace SteelWire.AppCode.Dependencies
{
    public abstract class DependencyCollection<TC, TI> : DependencyObject
        where TC : ICollection<TI>, new()
        where TI : new()
    {
        public event EventHandler ItemsChangedHandler;
        public static readonly DependencyPropertyKey ItemsPropertyKey;
        public static readonly DependencyProperty ItemsProperty;
        static DependencyCollection()
        {
            ItemsPropertyKey = DependencyProperty.RegisterReadOnly(
            "Items", typeof(TC), typeof(DependencyCollection<TC, TI>),
            new FrameworkPropertyMetadata
            {
                DefaultValue = new TC(),
                PropertyChangedCallback = ItemsPropertyChanged
            });
            ItemsProperty = ItemsPropertyKey.DependencyProperty;
        }
        private static void ItemsPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            DependencyCollection<TC, TI> dependency = (DependencyCollection<TC, TI>)sender;
            dependency.OnItemsChanged(EventArgs.Empty);
        }
        public TC Items
        {
            get { return (TC)this.ReadLocalValue(ItemsProperty); }
        }
        protected virtual void OnItemsChanged(EventArgs e)
        {
            EventHandler handler = ItemsChangedHandler;
            if (handler != null)
            {
                handler.Invoke(this, e);
            }
        }
        public virtual TI New()
        {
            TI item = new TI();
            Add(item);
            return item;
        }
        public virtual void Add(TI item)
        {
            this.Items.Add(item);
        }
        public virtual void AddRange(IEnumerable<TI> items)
        {
            foreach (TI item in items)
            {
                Add(item);
            }
        }
        public virtual void Remove(TI item)
        {
            if (this.Items.Contains(item))
            {
                this.Items.Remove(item);
            }
        }
        public virtual void Clear()
        {
            this.Items.Clear();
        }
        public void ResetLocalItems()
        {
            TC localValue = new TC();
            foreach (TI element in this.Items)
            {
                localValue.Add(element);
            }
            this.SetValue(ItemsPropertyKey, localValue);
        }
    }
}
