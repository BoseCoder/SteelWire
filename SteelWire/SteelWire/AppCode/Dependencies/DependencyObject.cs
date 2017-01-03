using System;
using System.Windows;

namespace SteelWire.AppCode.Dependencies
{
    /// <summary>
    /// 自定义依赖性
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DependencyObject<T> : DependencyObject
    {
        public event EventHandler ValueChangedHandler;

        public static DependencyProperty ValueProperty { get; } = DependencyProperty.Register("Value", typeof(T),
            typeof(DependencyObject<T>), new FrameworkPropertyMetadata
            {
                DefaultValue = default(T),
                PropertyChangedCallback = ValuePropertyChanged
            });

        private static void ValuePropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject<T> dependency = (DependencyObject<T>)sender;
            dependency.OnItemValueChanged(EventArgs.Empty);
        }
        public DependencyObject()
            : this(default(T))
        { }
        public DependencyObject(T defaultValue)
        {
            this.Value = defaultValue;
        }
        public T Value
        {
            get { return (T)this.GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
        private void OnItemValueChanged(EventArgs e)
        {
            this.ValueChangedHandler?.Invoke(this, e);
        }
    }
}