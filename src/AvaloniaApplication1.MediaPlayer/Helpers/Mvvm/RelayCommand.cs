using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;

namespace AvaloniaApplication1.MediaPlayer.Helpers.Mvvm
{
    internal class RelayCommand : ICommand
    {
        private readonly Action _execute;

        private readonly Func<bool>? _canExecute;

        /// <summary>
        /// Initializes a new instance of the RelayCommand class that
        /// can always execute.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <exception cref="ArgumentNullException">If the execute argument is null.</exception>
        public RelayCommand(Action execute)
            : this(execute, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the RelayCommand class.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <param name="canExecute">The execution status logic.</param>
        /// <exception cref="ArgumentNullException">If the execute argument is null.</exception>
        internal RelayCommand(Action execute, Func<bool>? canExecute)
        {
            execute.CheckNotNull(nameof(execute));

            _execute = execute;
            _canExecute = canExecute;
        }

#if SILVERLIGHT
        /// <summary>
        /// Occurs when changes occur that affect whether the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged;
#else
        /// <summary>
        /// Occurs when changes occur that affect whether the command should execute.
        /// </summary>
        public event EventHandler? CanExecuteChanged;
        // {
        //     add
        //     {
        //         if (_canExecute != null)
        //         {
        //             CommandManager.RequerySuggested += value;
        //         }
        //     }
        //
        //     remove
        //     {
        //         if (_canExecute != null)
        //         {
        //             CommandManager.RequerySuggested -= value;
        //         }
        //     }
        // }
#endif

        /// <summary>
        /// Raises the <see cref="CanExecuteChanged" /> event.
        /// </summary>
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic",
            Justification = "The this keyword is used in the Silverlight version")]
        [SuppressMessage("Microsoft.Design", "CA1030:UseEventsWhereAppropriate",
            Justification = "This cannot be an event")]
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <param name="parameter">This parameter will always be ignored.</param>
        /// <returns>true if this command can be executed; otherwise, false.</returns>
        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            return _canExecute?.Invoke() ?? true;
        }

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">This parameter will always be ignored.</param>
        public void Execute(object parameter) => _execute();
    }

}
