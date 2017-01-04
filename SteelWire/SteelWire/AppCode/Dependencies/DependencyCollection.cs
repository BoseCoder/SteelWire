using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;

namespace SteelWire.AppCode.Dependencies
{
    public class DependencyCollection<T> : DependencyCollection<List<DependencyObject<T>>, DependencyObject<T>>
    {
        public virtual void Add(T item)
        {
            DependencyObject<T> dependencyObject = new DependencyObject<T>(item);
            this.Items.Add(dependencyObject);
            this.OnItemsChanged(EventArgs.Empty);
        }
        public virtual void AddRange(IEnumerable<T> items)
        {
            if (items == null)
            {
                return;
            }
            foreach (T item in items)
            {
                DependencyObject<T> dependencyObject = new DependencyObject<T>(item);
                this.Items.Add(dependencyObject);
            }
            this.OnItemsChanged(EventArgs.Empty);
        }
        public virtual void Remove(T item)
        {
            List<DependencyObject<T>> removeItems = this.Items.Where(i => Equals(i.Value, item)).ToList();
            if (removeItems.Any())
            {
                foreach (DependencyObject<T> removeItem in removeItems)
                {
                    this.Items.Remove(removeItem);
                }
                this.OnItemsChanged(EventArgs.Empty);
            }
        }
    }

    /// <summary>
    /// 自定义集合依赖项
    /// </summary>
    /// <typeparam name="TC"></typeparam>
    /// <typeparam name="TI"></typeparam>
    public class DependencyCollection<TC, TI> : DependencyObject
        where TC : ICollection<TI>, new()
    {
        public event EventHandler ItemsChangedHandler;

        public static DependencyPropertyKey ItemsPropertyKey { get; } = DependencyProperty.RegisterReadOnly("Items",
            typeof(TC), typeof(DependencyCollection<TC, TI>), new FrameworkPropertyMetadata
            {
                DefaultValue = new TC(),
                PropertyChangedCallback = ItemsPropertyChanged
            });
        private static void ItemsPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            DependencyCollection<TC, TI> dependency = (DependencyCollection<TC, TI>)sender;
            dependency.ItemsChangedHandler?.Invoke(dependency, EventArgs.Empty);
        }
        public TC Items => (TC)this.ReadLocalValue(ItemsPropertyKey.DependencyProperty);

        protected virtual void OnItemsChanged(EventArgs e)
        {
            TC items = this.Items;
            this.ClearValue(ItemsPropertyKey);
            this.SetValue(ItemsPropertyKey, items);
            this.ItemsChangedHandler?.Invoke(this, e);
        }

        public DependencyCollection()
        {
            this.SetValue(ItemsPropertyKey, new TC());
        }

        public virtual void Add(TI item)
        {
            this.Items.Add(item);
            OnItemsChanged(EventArgs.Empty);
        }
        public virtual void AddRange(IEnumerable<TI> items)
        {
            if (items == null)
            {
                return;
            }
            foreach (TI item in items)
            {
                Add(item);
            }
            OnItemsChanged(EventArgs.Empty);
        }
        public virtual void Remove(TI item)
        {
            if (item == null)
            {
                return;
            }
            if (this.Items.Contains(item))
            {
                this.Items.Remove(item);
                OnItemsChanged(EventArgs.Empty);
            }
        }
        public virtual void Clear()
        {
            this.Items.Clear();
            OnItemsChanged(EventArgs.Empty);
        }
    }
}
