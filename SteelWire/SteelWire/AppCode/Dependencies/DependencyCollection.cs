using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Linq;

namespace SteelWire.AppCode.Dependencies
{
    public class DependencyCollection<T> : DependencyCollection<List<DependencyObject<T>>, DependencyObject<T>>
    {
        protected virtual void AddItem(T item)
        {
            DependencyObject<T> dependencyObject = new DependencyObject<T>(item);
            this.Items.Add(dependencyObject);
        }
        public void Add(T item)
        {
            this.AddItem(item);
            this.OnItemsChanged(EventArgs.Empty);
        }
        public void AddRange(IEnumerable<T> items)
        {
            if (items == null)
            {
                return;
            }
            foreach (T item in items)
            {
                this.AddItem(item);
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
    public class DependencyCollection<TC, TI> : DependencyObject, IEnumerable<TI>
        where TC : class, ICollection<TI>, new()
        where TI : DependencyObject
    {
        private bool _isCollectionObjectChanging;
        public event EventHandler ItemsChangedHandler;

        public static DependencyPropertyKey ItemsPropertyKey { get; } = DependencyProperty.RegisterReadOnly("Items",
            typeof(TC), typeof(DependencyCollection<TC, TI>), new FrameworkPropertyMetadata
            {
                DefaultValue = new TC(),
                PropertyChangedCallback = CollectionObjectPropertyChanged
            });
        private static void CollectionObjectPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            DependencyCollection<TC, TI> dependency = (DependencyCollection<TC, TI>)sender;
            if (!dependency._isCollectionObjectChanging)
            {
                dependency.OnItemsChangedHandler(EventArgs.Empty);
            }
        }

        public TC Items => this.ReadLocalValue(ItemsPropertyKey.DependencyProperty) as TC ?? new TC();

        protected virtual void OnItemsChanged(EventArgs e)
        {
            this._isCollectionObjectChanging = true;
            TC items = this.Items;
            this.ClearValue(ItemsPropertyKey);
            this.SetValue(ItemsPropertyKey, items);
            OnItemsChangedHandler(e);
            this._isCollectionObjectChanging = false;
        }

        protected void OnItemsChangedHandler(EventArgs e)
        {
            this.ItemsChangedHandler?.Invoke(this, e);
        }

        public DependencyCollection()
        {
            this.SetValue(ItemsPropertyKey, new TC());
        }
        protected virtual void AddItem(TI item)
        {
            this.Items.Add(item);
        }
        public virtual void Add(TI item)
        {
            this.AddItem(item);
            this.OnItemsChanged(EventArgs.Empty);
        }
        public virtual void AddRange(IEnumerable<TI> items)
        {
            if (items == null)
            {
                return;
            }
            foreach (TI item in items)
            {
                this.AddItem(item);
            }
            this.OnItemsChanged(EventArgs.Empty);
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
                this.OnItemsChanged(EventArgs.Empty);
            }
        }
        public virtual void Clear()
        {
            this.Items.Clear();
            this.OnItemsChanged(EventArgs.Empty);
        }

        public IEnumerator<TI> GetEnumerator()
        {
            return this.Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
