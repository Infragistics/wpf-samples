using System;
using System.Windows.Input;

namespace IGRibbon.Model
{
    public class SimpleCommand : ICommand
    {
        readonly Action<object> _execute;
        readonly Func<object, Boolean> _canExecute;

        public SimpleCommand(Action<object> e, Func<object, Boolean> ce)
        {
            if (e == null)
            {
                throw new ArgumentNullException("e", @"Action argument cannot be null");
            }

            _execute = e;
            _canExecute = ce;
        }

        public SimpleCommand(Action<object> e) : this(e, null)
        {
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecute == null)
            {
                return true;
            }
           
            return _canExecute(parameter);
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            _execute(parameter);
        }
    }
}