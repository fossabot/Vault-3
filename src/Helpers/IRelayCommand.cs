using System.Windows.Input;

namespace Seemon.Vault.Helpers
{
    public interface IRelayCommand : ICommand
    {
        public void RaiseCanExecuteChanged();
    }
}
