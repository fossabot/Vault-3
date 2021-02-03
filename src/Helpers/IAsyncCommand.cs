/// https://johnthiriet.com/mvvm-going-async-with-async-command/
using System.Threading.Tasks;
using System.Windows.Input;

namespace Seemon.Vault.Helpers
{
    public interface IAsyncCommand : IRelayCommand
    {
        Task ExecuteAsync();
        bool CanExecute();
    }

    public interface IAsyncCommand<T> : IRelayCommand
    {
        Task ExecuteAsync(T parameter);
        bool CanExecute(T parameter);
    }
}
