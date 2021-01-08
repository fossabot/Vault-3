using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace Seemon.Vault.Helpers
{
    public class ViewModelBase : BindableBase
    {
        private Window _owner;
        private IList<ICommand> _commands;
        private bool _isDirty = false;

        public ViewModelBase(Window owner)
        {
            _owner = owner;
            _commands = new List<ICommand>();
        }

        public Window Owner
        {
            get { return _owner; }
            set { _owner = value; }
        }

        public IList<ICommand> Commands
        {
            get { return _commands; }
        }

        public bool IsDirty
        {
            get { return _isDirty; }
            set
            {
                SetProperty(ref _isDirty, value);
                RaiseCommandsCanExecute();
            }
        }

        public bool TryClose()
        {
            try
            {
                if (_owner != null)
                    Owner.Close();
                return true;
            }
            catch { }
            return false;
        }

        public RelayCommand RegisterCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            var command = new RelayCommand(execute, canExecute);
            _commands.Add(command);
            return command;
        }

        public void RaiseCommandsCanExecute()
        {
            foreach (RelayCommand command in _commands)
            {
                command.RaiseCanExecuteChanged();
            }
        }

        public override void Validate([CallerMemberName] string propertyName = null)
        {
            RaiseCommandsCanExecute();
            base.Validate(propertyName);
        }
    }
}
