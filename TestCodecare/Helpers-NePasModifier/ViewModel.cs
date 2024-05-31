using System.ComponentModel;
using System.Linq.Expressions;
using System.Windows.Threading;

namespace TestCodecare.Helpers_NePasModifier
{
    public abstract class ViewModel : IDisposable, INotifyPropertyChanged
    {
        private readonly Dictionary<string, PropertyChangedEventArgs> _propertyChangedEventArgsCache;

        protected ViewModel() : base()
        {
            _propertyChangedEventArgsCache = new Dictionary<string, PropertyChangedEventArgs>();
            IsInitialized = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnInitializeViewModel()
        {
        }

        public bool IsInitialized { get; private set; }

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Dispose any managed objects
                    // ...
                }

                _propertyChangedEventArgsCache.Clear();

                // Now disposed of any unmanaged objects
                // ...

                _disposed = true;
            }
        }

        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~ViewModel()
        {
            Dispose(false);
        }

        public void InitializeViewModel()
        {
            if (!IsInitialized)
            {
                IsInitialized = true;
                OnInitializeViewModel();
            }
        }

        private PropertyChangedEventArgs GetCachedPropertyChangedEventArgs(string propertyName)
        {
            if (!_propertyChangedEventArgsCache.TryGetValue(propertyName, out var pcea))
            {
                pcea = new PropertyChangedEventArgs(propertyName);
                _propertyChangedEventArgsCache[propertyName] = pcea;
            }

            return pcea;
        }

        protected void RaiseCommandChanged(string propertyName)
        {
            var property = GetType().GetProperty(propertyName);
            if (property != null)
            {
                var command = property.GetValue(this, null);
                if (command != null)
                {
                    property.SetValue(this, null, null);
                    property.SetValue(this, command, null);
                }
            }
        }

        protected void RaisePropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;

            if (handler != null)
            {
                Dispatcher.CurrentDispatcher.InvokeIfRequired(
                    () => { handler(this, GetCachedPropertyChangedEventArgs(propertyName)); },
                    DispatcherPriority.Normal);
            }
        }

        protected void RaisePropertyChanged<TProperty>(Expression<Func<TProperty>> propertyExpression)
        {
            var propertyName = ((MemberExpression)propertyExpression.Body).Member.Name;

            var handler = PropertyChanged;

            if (handler != null)
            {
                Dispatcher.CurrentDispatcher.InvokeIfRequired(
                    () => { handler(this, GetCachedPropertyChangedEventArgs(propertyName)); },
                    DispatcherPriority.Normal);
            }
        }

        private bool SetValue<T>(ref T oldValue, T newValue)
        {
            if (oldValue == null && newValue == null)
            {
                return false;
            }

            if (oldValue == null && newValue != null || !oldValue.Equals(newValue))
            {
                oldValue = newValue;
                return true;
            }

            return false;
        }

        protected bool SetValueAndRaiseEventIfPropertyChanged<T>(Expression<Func<T>> propertyExpression, ref T oldValue,
            T newValue)
        {
            if (SetValue(ref oldValue, newValue))
            {
                RaisePropertyChanged(propertyExpression);
                return true;
            }

            return false;
        }

        protected bool SetValueAndRaiseEventIfPropertyChanged<T>(string propertyName, ref T oldValue, T newValue)
        {
            if (SetValue(ref oldValue, newValue))
            {
                RaisePropertyChanged(propertyName);
                return true;
            }

            return false;
        }
    }

    public static class DispatcherExtensions
    {
        public static void InvokeIfRequired(this Dispatcher dispatcher, Action action, DispatcherPriority priority)
        {
            if (dispatcher.CheckAccess())
            {
                action();
            }
            else
            {
                dispatcher.Invoke(action, priority);
            }
        }
    }
}
