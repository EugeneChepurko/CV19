﻿using CV19.Infrastructure.Commands.Base;
using System;

namespace CV19.Infrastructure.Commands
{
    internal class RelayCommand : Command
    {
        private readonly Action<object> _execute = null;
        private readonly Func<object, bool> _canExecute = null;

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }
        public override bool CanExecute(object parameter) => _canExecute?.Invoke(parameter) ?? true;

        public override void Execute(object parameter) => _execute(parameter);
    }
}