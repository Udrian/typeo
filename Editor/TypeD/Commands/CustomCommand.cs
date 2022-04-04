﻿using System;
using System.Windows.Input;
using TypeD.Models.Interfaces;

namespace TypeD.Commands
{
    public class CustomCommand : ICommand
    {
        // Models
        public IResourceModel ResourceModel { get; private set; }

        private readonly Predicate<object> canExecute;
        private readonly Action<object> execute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        // Constructors
        public CustomCommand(IResourceModel resourceModel = null) : this(null, null, resourceModel) { }
        public CustomCommand(Action<object> execute, IResourceModel resourceModel = null) : this(execute, null, resourceModel) { }
        public CustomCommand(Action<object> execute, Predicate<object> canExecute, IResourceModel resourceModel = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
            ResourceModel = resourceModel;
        }

        // Functions
        public virtual bool CanExecute(object parameter)
        {
            if (canExecute == null)
            {
                return true;
            }

            return canExecute(parameter);
        }

        public virtual void Execute(object parameter)
        {
            execute(parameter);
        }

        public void RaiseCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }
    }
}
