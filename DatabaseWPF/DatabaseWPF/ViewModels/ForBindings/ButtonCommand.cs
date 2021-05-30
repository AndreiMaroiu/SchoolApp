﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DatabaseWPF.ViewModels
{ 
    public class ButtonCommand : ICommand
    {
        public static readonly Predicate<object> defaultCanExecute = (object o) => true;

        private Predicate<object> canExecute;
        private Action<object> execute;

        public ButtonCommand(Action<object> executeAction, Predicate<object> canExecutePredicate = null)
            => (execute, canExecute) = (executeAction, canExecutePredicate);

        #region Command

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public bool CanExecute(object parameter)
            => (canExecute is null) ? true : canExecute(parameter);

        public void Execute(object parameter)
            => execute(parameter);

        #endregion
    }
}
