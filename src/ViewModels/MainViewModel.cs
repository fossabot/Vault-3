using Seemon.Vault.Helpers;
using System.Windows.Input;

namespace Seemon.Vault.ViewModels
{
    public class MainViewModel : BindableBase
    {
        private bool _showAbout = false;
        public bool ShowAbout
        {
            get { return _showAbout; }
            set 
            {
                SetProperty(ref _showAbout, value);
                ((RelayCommand)this.ShowAboutCommand).RaiseCanExecuteChanged();
            }
        }

        private RelayCommand _showAboutCommand;
        public ICommand ShowAboutCommand
        {
            get
            {
                if (_showAboutCommand == null)
                    _showAboutCommand = new RelayCommand(OnShowAbout);
                return _showAboutCommand;
            }
        }

        public void OnShowAbout(object parameter)
        {
            this.ShowAbout = !this.ShowAbout;  
        }
    }
}