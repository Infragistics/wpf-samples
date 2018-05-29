using System;
using System.Diagnostics;
using System.Windows.Input;

namespace IGExtensions.Common.Commands
{
    /// <summary>
    /// A command whose sole purpose is to relay its functionality to other
    /// objects by invoking delegates. The default return value for the CanExecute
    /// method is 'true'.  This class allows you to accept command parameters in the
    /// Execute and CanExecute callback methods.
    /// </summary>
    public class RelayCommand<T> : ICommand
    {
        #region Fields

        readonly Action<T>		_execute = null;
        readonly Predicate<T>	_canExecute = null;

        #endregion // Fields

        #region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="RelayCommand&lt;T&gt;"/> class.
		/// </summary>
		/// <param name="execute">The execute.</param>
        public RelayCommand(Action<T> execute)
            : this(execute, null)
        {
        }

        /// <summary>
        /// Creates a new command.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <param name="canExecute">The execution status logic.</param>
        public RelayCommand(Action<T> execute, Predicate<T> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");

            _execute = execute;
            _canExecute = canExecute;
        }

        #endregion // Constructors

        #region ICommand Members

		/// <summary>
		/// Defines the method that determines whether the command can execute in its current state.
		/// </summary>
		/// <param name="parameter">Data used by the command. If the command does not require data to be passed, this object can be set to null.</param>
		/// <returns>
		/// true if this command can be executed; otherwise, false.
		/// </returns>
        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute(SafeConvert(parameter));
        }

        public event EventHandler CanExecuteChanged;
        public void OnCanExecuteChanged(EventArgs e)
        {
            if(CanExecuteChanged != null)
            {
                CanExecuteChanged(this, e);
            }
        }
        // WPF
        //public event EventHandler CanExecuteChanged
        //{
        //    add { CommandManager.RequerySuggested += value; }
        //    remove { CommandManager.RequerySuggested -= value; }
        //}

		/// <summary>
		/// Defines the method to be called when the command is invoked.
		/// </summary>
		/// <param name="parameter">Data used by the command. If the command does not require data to be passed, this object can be set to null.</param>
        public void Execute(object parameter)
        {
			_execute(SafeConvert(parameter));
        }
        #endregion // ICommand Members

		#region Private Methods
		/// <summary>
		/// Converts the value to type T.  If the conversion is not possible
		/// a null value is returned (instead of an exception)
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		private static T SafeConvert(object value)
		{
			if (value == null) return default(T);
			if (value.GetType() == typeof(T)) return (T)value;

			T p = default(T);

			try
			{
				p = (T)Convert.ChangeType(value, typeof(T), null);
			}
			catch (InvalidCastException)
			{
				Debug.Assert(true, string.Format("Invalid parameter object type. Expected type: {0}.  Actial type: {1}.", typeof(T).FullName, value != null ? value.GetType().FullName : "N/A"));
			}
			catch { }

			return p;
		}
		#endregion Private Methods
	}
}
