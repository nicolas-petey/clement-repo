using System.Diagnostics;
using System.Windows.Input;

namespace TestCodecare.Helpers_NePasModifier
{
    public class DelegatingCommand<T> : ICommand
    {
        readonly Predicate<T> _canExecute;
        readonly Action<T> _execute;


        /// <summary>
        /// Creates a new command.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <param name="canExecute">The execution status logic.</param>
        internal DelegatingCommand(Action<T> execute, Predicate<T> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException("execute");
            _canExecute = canExecute;
        }

        /// <summary>
        /// Creates a new command that can always execute.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        internal DelegatingCommand(Action<T> execute)
            : this(execute, null)
        {
        }


        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }


        [DebuggerStepThrough]
        public bool CanExecute(T parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        public void Execute(T parameter)
        {

            _execute(parameter);
        }


        bool ICommand.CanExecute(object parameter)
        {
            if (!parameter.IsDisconnectedItem() && parameter.TryCast<T>(out var o))
            {
                return CanExecute((T)parameter);
            }
            else
            {
                return false;
            }
        }

        void ICommand.Execute(object parameter)
        {
            Execute((T)parameter);
        }
    }

    public static class BindingExpressionBaseExtensions
    {
        public static bool IsDisconnectedItem(this object o)
        {
            if (ReferenceEquals(o, null))
            {
                return false;
            }
            else
            {
                var disconnectedItem = typeof(System.Windows.Data.BindingExpressionBase).GetField("DisconnectedItem",
                    System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic)
                    ?.GetValue(null);
                return o == disconnectedItem;
            }
        }
    }

    public static class ExtensionsOfObject
    {
        public static object Cast(this object o, Type typeToCast)
        {
            if (typeToCast == null)
            {
                throw new ArgumentNullException("typeToCast");
            }

            if (typeToCast.IsInstanceOfType(o))
            {
                return o;
            }

            if (ReferenceEquals(o, null) || Equals(o, DBNull.Value))
            {
                if (typeToCast.IsClass)
                {
                    return null;
                }

                return
                    Activator.CreateInstance(typeToCast); // NOTE: Bug if typeToCast is a struct with no construtor without arguments
            }

            if (o is IConvertible)
            {
                return Convert.ChangeType(o, typeToCast);
            }

            throw new ArgumentException(
                $"Can't cast object of type {o.GetType().FullName} to type {typeToCast.FullName}");
        }

        public static bool TryCast(this object o, Type typeToCast, out object value)
        {
            value = null;
            try
            {
                value = o.Cast(typeToCast);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool TryCast<T>(this object o, out T value)
        {
            value = default(T);
            var castSucceed = o.TryCast(typeof(T), out var castValue);

            if (castValue is T)
            {
                value = (T)castValue;
            }

            return castSucceed;
        }
    }
}
