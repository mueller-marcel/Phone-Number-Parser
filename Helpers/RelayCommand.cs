using System;
using System.Windows.Input;

namespace PhoneNumberParser.Helpers
{
    public class RelayCommand : ICommand
    {
        #region Fields
        /// <summary>
        /// Delegate for the execute handler
        /// </summary>
        private readonly Action<object> _executeHandler;

        /// <summary>
        /// Delegate for the canExecute Handler
        /// </summary>
        private readonly Predicate<object> _canExecuteHandler;
        #endregion

        #region Events
        /// <summary>
        /// Handles the canExecuteChanged event
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor for the execute handler that calles the constructor with both arguments.
        /// Then can execute handler is null then
        /// </summary>
        /// <param name="execute">The handler for the execute delegate</param>
        public RelayCommand(Action<object> execute) : this(execute, null)
        {

        }

        /// <summary>
        /// Constructor that sets the execute and the can execute handler
        /// </summary>
        /// <param name="execute">The handler for the execute delegate</param>
        /// <param name="canExecute">The handler for the can execute delegate</param>
        /// <exception cref="ArgumentNullException">Thrown, when the execute delegate argument is null</exception>
        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute delegate is null");
            }

            _executeHandler = execute;
            _canExecuteHandler = canExecute;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Returns the value of the can execute delegate. If the delegate is null true will be returned
        /// </summary>
        /// <param name="parameter">Additional information for the delegate</param>
        /// <returns>The return value of the can execute delegate or true if null</returns>
        public bool CanExecute(object parameter)
        {
            if (_canExecuteHandler == null)
            {
                return true;
            }
            return _canExecuteHandler(parameter);
        }

        /// <summary>
        /// Executes the execute delegate
        /// </summary>
        /// <param name="parameter">Additional information for the delegate</param>
        public void Execute(object parameter)
        {
            _executeHandler(parameter);
        }
        #endregion
    }
}
