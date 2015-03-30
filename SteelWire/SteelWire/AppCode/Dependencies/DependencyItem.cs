using System;
using System.Windows;

namespace SteelWire.AppCode.Dependencies
{
    public class DependencyItem<T> : DependencyObject
    {
        public event EventHandler ItemValueChangedHandler;
        public static readonly DependencyProperty ItemValueProperty = DependencyProperty.Register(
            "ItemValue", typeof(T), typeof(DependencyItem<T>),
            new FrameworkPropertyMetadata
            {
                DefaultValue = default(T),
                PropertyChangedCallback = ItemValuePropertyChanged
            });
        private static void ItemValuePropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            DependencyItem<T> dependency = (DependencyItem<T>)sender;
            dependency.OnItemValueChanged(EventArgs.Empty);
        }
        public DependencyItem()
        { }
        public DependencyItem(T defaultValue)
        {
            SetValue(ItemValueProperty, defaultValue);
        }
        public T ItemValue
        {
            get { return (T)this.GetValue(ItemValueProperty); }
            set { SetValue(ItemValueProperty, value); }
        }
        private void OnItemValueChanged(EventArgs e)
        {
            EventHandler handler = ItemValueChangedHandler;
            if (handler != null)
            {
                handler.Invoke(this, e);
            }
        }
    }
}
