using Seemon.Vault.Helpers;
using System.Windows;
using System.Windows.Input;

namespace Seemon.Vault.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private bool _showAbout = false;
        public bool ShowAbout
        {
            get { return _showAbout; }
            set
            {
                SetProperty(ref _showAbout, value);
                ((RelayCommand)ShowAboutCommand).RaiseCanExecuteChanged();
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

        private RelayCommand _showSettingsCommand;
        public ICommand ShowSettingsCommand
        {
            get
            {
                if (_showSettingsCommand == null)
                    _showSettingsCommand = new RelayCommand(OnShowSettings);
                return _showSettingsCommand;
            }
        }

        public MainViewModel(Window owner)
            : base(owner) { }

        public void OnShowAbout(object parameter)
        {
            ShowAbout = true;
        }

        public void OnShowSettings(object parameter)
        {
            Views.Settings settingsWindow = new Views.Settings();
            settingsWindow.Owner = Owner;
            settingsWindow.ShowDialog();
        }
    }
}