using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Win32;
using Seemon.Vault.Helpers;
using Seemon.Vault.Models;
using System.Windows;
using System.Windows.Input;

namespace Seemon.Vault.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        public SettingsViewModel(Window owner)
            : base(owner)
        {
            _settings = SettingsManager.Instance.Settings.DeepCopy();


            _settings.System.PropertyChanged += OnPropertyChanged;
            _settings.Clipboard.PropertyChanged += OnPropertyChanged;
            _settings.PasswordGenerator.PropertyChanged += OnPropertyChanged;
            _settings.Git.PropertyChanged += OnPropertyChanged;
            _settings.Programs.PropertyChanged += OnPropertyChanged;
        }

        private Settings _settings;
        public Settings Settings
        {
            get { return _settings; }
            set { _settings = value; }
        }

        private bool _isDirty = false;
        public bool IsDirty
        {
            get { return _isDirty; }
            set
            {
                _isDirty = value;
                _updateCommand.RaiseCanExecuteChanged();
            }
        }

        private RelayCommand _updateCommand;
        public ICommand UpdateCommand
        {
            get
            {
                if (_updateCommand == null)
                    _updateCommand = new RelayCommand(OnUpdate, CanUpdate);
                return _updateCommand;
            }
        }

        private RelayCommand _cancelCommand;
        public ICommand CancelCommand
        {
            get
            {
                if (_cancelCommand == null)
                    _cancelCommand = new RelayCommand(OnCancel);
                return _cancelCommand;
            }
        }

        private RelayCommand _browseCommand;
        public ICommand BrowseCommand
        {
            get
            {
                if (_browseCommand == null)
                    _browseCommand = new RelayCommand(OnBrowse);
                return _browseCommand;
            }
        }

        public void OnUpdate(object parameter)
        {
            SettingsManager.Instance.SaveSettings(_settings);
            TryClose();
        }

        private bool CanUpdate(object commandParameter)
        {
            return IsDirty;
        }

        public async void OnCancel(object parameter)
        {
            bool okToClose = true;
            if (IsDirty)
            {
                var options = new MetroDialogSettings
                {
                    AffirmativeButtonText = "Yes",
                    NegativeButtonText = "No",
                };
                var result = await ((MetroWindow)this.Owner).ShowMessageAsync("Vault settings changed!", "Are you sure you want to close and lose the changes you made?", MessageDialogStyle.AffirmativeAndNegative, options);
                if (result != MessageDialogResult.Affirmative)
                    okToClose = false;
            }

            if (okToClose)
                TryClose();
        }

        public void OnBrowse(object parameter)
        {
            string filter = (parameter.ToString() == "GPG") ? "GPG Executable|gpg.exe|" : "GIT Executable|git.exe|";
            var path = ShowDialog(filter);
            if (!string.IsNullOrEmpty(path))
            {
                if (parameter.ToString() == "GPG")
                    _settings.Programs.Gpg = path;
                else
                    _settings.Programs.Git = path;
            }
        }

        private void OnPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            IsDirty = true;
        }

        private string ShowDialog(string filter)
        {
            string path = string.Empty;
            var dialog = new OpenFileDialog();
            dialog.Title = "Select Program";
            dialog.Filter = filter + "All Files (*.*)|*.*";
            if (dialog.ShowDialog() == true)
            {
                path = dialog.FileName;
            }
            return path;
        }
    }
}