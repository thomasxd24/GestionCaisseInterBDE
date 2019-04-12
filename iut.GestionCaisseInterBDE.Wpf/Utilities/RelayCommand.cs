using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace iut.GestionCaisseInterBDE.Wpf.Utilities
{
    public class RelayCommand<T> : ICommand
    {
        private static bool CanExecute(T parameter)
        {
            return true;
        }

        readonly Action<T> mExecute;

        readonly Func<T, bool> mCanExecute;

        public RelayCommand(Action<T> execute, Func<T, bool> canExecute = null)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }
            mExecute = execute;
            mCanExecute = canExecute ?? CanExecute;
        }

        public bool CanExecute(object parameter)
        {
            return mCanExecute(TranslateParameter(parameter));
        }

        public event EventHandler CanExecuteChanged 
        {
            add
            {
                if (mCanExecute != null)
                {
                    CommandManager.RequerySuggested += value;
                }
            }
            remove
            {
                if (mCanExecute != null)
                {
                    CommandManager.RequerySuggested -= value;
                }
            }
        }

        public void Execute(object parameter) 
        {
            mExecute(TranslateParameter(parameter));
        }
        
        private T TranslateParameter(object parameter) 
        {
            T value = default(T);
            if (parameter != null && typeof(T).IsEnum)
            {
                value = (T)Enum.Parse(typeof(T), (string)parameter);
            }
            else
            {
                value = (T)parameter;
            }
            return value;
        }
    }

    public class RelayCommand : RelayCommand<object>
    {
        public RelayCommand(Action execute, Func<bool> canExecute = null)
            : base(obj => execute(), (canExecute == null ? null : new Func<object, bool>(obj => canExecute())))
        {
        }
    }
}
