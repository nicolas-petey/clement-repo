using System.Windows.Input;

namespace TestCodecare.Helpers_NePasModifier
{
    public static class CommandHelper
    {
        public static ICommand Create(Action execute)
        {
            return new DelegatingCommand<object>(nu => execute());
        }

        public static ICommand Create(Action execute, Func<bool> canExecute)
        {
            return new DelegatingCommand<object>(nu => execute(), nu => canExecute());
        }

        public static DelegatingCommand<T> Create<T>(Action<T> execute)
        {
            return new DelegatingCommand<T>(execute);
        }

        public static DelegatingCommand<T> Create<T>(Action<T> execute, Predicate<T> canExecute)
        {
            return new DelegatingCommand<T>(execute, canExecute);
        }
    }
}
