using Seemon.Vault.Helpers;
using System.Windows;
using System.Windows.Input;

namespace Seemon.Vault.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private bool _showAbout = false;
        private RelayCommand _showAboutCommand;
        private RelayCommand _showSettingsCommand;

        public MainViewModel(Window owner)
            : base(owner) { }

        public bool ShowAbout
        {
            get { return _showAbout; }
            set { SetProperty(ref _showAbout, value); }
        }

        public ICommand ShowAboutCommand
        {
            get
            {
                if (_showAboutCommand == null)
                    _showAboutCommand = RegisterCommand(OnShowAbout);
                return _showAboutCommand;
            }
        }

        public ICommand ShowSettingsCommand
        {
            get
            {
                if (_showSettingsCommand == null)
                    _showSettingsCommand = RegisterCommand(OnShowSettings);
                return _showSettingsCommand;
            }
        }

        public void OnShowAbout(object parameter) => ShowAbout = true;

        public void OnShowSettings(object parameter)
        {
            var settingsWindow = new Views.Settings();
            settingsWindow.Owner = Owner;
            settingsWindow.ShowDialog();
        }
    }
}
