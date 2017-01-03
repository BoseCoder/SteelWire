using System;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace SteelWire.AppCode.Data
{
    /// <summary>
    /// 时钟
    /// </summary>
    public class TimeMeter : DependencyObject
    {
        private delegate void SetTimePropertyDelegate(DateTime now);
        private readonly object _timeLocker = new object();
        public event EventHandler CurrentTimeChangedHandler;
        private static readonly DependencyProperty CurrentTimeProperty;

        /// <summary>
        /// 单例
        /// </summary>
        public static TimeMeter OnceInstance { get; } = new TimeMeter();

        /// <summary>
        /// 静态构造
        /// </summary>
        static TimeMeter()
        {
            DateTime now = DateTime.Now;
            CurrentTimeProperty = DependencyProperty.Register("CurrentTime", typeof(DateTime), typeof(TimeMeter),
                new FrameworkPropertyMetadata
                {
                    DefaultValue = now,
                    PropertyChangedCallback = OnCurrentTimePropertyChanged
                });
            OnceInstance.CurrentTime = now;
        }

        /// <summary>
        /// 依赖项修改触发事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void OnCurrentTimePropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            TimeMeter timeMeter = (TimeMeter)sender;
            timeMeter.OnCurrentTimeChanged(EventArgs.Empty);
        }

        /// <summary>
        /// 当前时间
        /// </summary>
        public DateTime CurrentTime
        {
            get { return (DateTime)this.GetValue(CurrentTimeProperty); }
            set
            {
                lock (this._timeLocker)
                {
                    SetValue(CurrentTimeProperty, value);
                }
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        private TimeMeter()
        {
            Timer timer = new Timer(TimerTicked);
            timer.Change(0, 30000);
        }

        /// <summary>
        /// 当前时间修改触发事件
        /// </summary>
        /// <param name="e"></param>
        private void OnCurrentTimeChanged(EventArgs e)
        {
            this.CurrentTimeChangedHandler?.Invoke(this, e);
        }

        /// <summary>
        /// 定时刷新时间（异步）
        /// </summary>
        /// <param name="state"></param>
        private void TimerTicked(object state)
        {
            SetTimePropertyDelegate method = SetTime;
            this.Dispatcher.BeginInvoke(method, DispatcherPriority.Send, DateTime.Now);
        }

        /// <summary>
        /// 定时刷新时间
        /// </summary>
        /// <param name="now"></param>
        private void SetTime(DateTime now)
        {
            SetValue(CurrentTimeProperty, now);
        }
    }
}
