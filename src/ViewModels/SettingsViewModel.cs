using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Win32;
using Seemon.Vault.Helpers;
using Seemon.Vault.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Seemon.Vault.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        private Settings _settings;
        private RelayCommand _updateCommand;
        private RelayCommand _cancelCommand;
        private RelayCommand _browseCommand;
        private RelayCommand _newProfileCommand;
        private RelayCommand _editProfileCommand;
        private RelayCommand _deleteProfileCommand;
        private RelayCommand _defaultProfileCommand;
        private RelayCommand _selectionChangedCommand;
        private Profile _selectedProfile;

        public SettingsViewModel(Window owner)
            : base(owner)
        {
            _settings = SettingsManager.Instance.Settings;

            _settings.System.PropertyChanged += OnPropertyChanged;
            _settings.Clipboard.PropertyChanged += OnPropertyChanged;
            _settings.PasswordGenerator.PropertyChanged += OnPropertyChanged;
            _settings.Git.PropertyChanged += OnPropertyChanged;
            _settings.Updates.PropertyChanged += OnPropertyChanged;
            _settings.Programs.PropertyChanged += OnPropertyChanged;
            _settings.Profiles.CollectionChanged += OnCollectionChanged;
        }

        public Settings Settings
        {
            get { return _settings; }
        }

        public IEnumerable<Profile> SelectedItems
        {
            get
            {
                return _settings.Profiles.Where(x => x.IsSelected);
            }
        }

        public Profile SelectedProfile
        {
            get { return _selectedProfile; }
            set
            {
                SetProperty(ref _selectedProfile, value);
                RaiseCommandsCanExecute();
            }
        }

        public ICommand UpdateCommand
        {
            get
            {
                if (_updateCommand == null)
                    _updateCommand = RegisterCommand(OnUpdate, CanUpdate);
                return _updateCommand;
            }
        }

        public ICommand CancelCommand
        {
            get
            {
                if (_cancelCommand == null)
                    _cancelCommand = RegisterCommand(OnCancel);
                return _cancelCommand;
            }
        }

        public ICommand BrowseCommand
        {
            get
            {
                if (_browseCommand == null)
                    _browseCommand = RegisterCommand(OnBrowse);
                return _browseCommand;
            }
        }

        public ICommand NewProfileCommand
        {
            get
            {
                if (_newProfileCommand == null)
                    _newProfileCommand = RegisterCommand(OnNewProfile);
                return _newProfileCommand;
            }
        }

        public ICommand EditProfileCommand
        {
            get
            {
                if (_editProfileCommand == null)
                    _editProfileCommand = RegisterCommand(OnEditProfile, CanEditProfile);
                return _editProfileCommand;
            }
        }

        public ICommand DeleteProfileCommand
        {
            get
            {
                if (_deleteProfileCommand == null)
                    _deleteProfileCommand = RegisterCommand(OnDeleteProfile, CanDeleteProfile);
                return _deleteProfileCommand;
            }
        }

        public ICommand DefaultProfileCommand
        {
            get
            {
                if (_defaultProfileCommand == null)
                    _defaultProfileCommand = RegisterCommand(OnDefaultProfile, CanDefaultProfile);
                return _defaultProfileCommand;
            }
        }

        public ICommand SelectionChangedCommand
        {
            get
            {
                if (_selectionChangedCommand == null)
                    _selectionChangedCommand = RegisterCommand(OnSelectionChanged);
                return _selectionChangedCommand;
            }
        }

        public new bool HasErrors
        {
            get { return Settings.Programs.HasErrors; }
        }

        public void OnUpdate(object parameter)
        {
            SettingsManager.Instance.SaveSettings(_settings);
            TryClose();
        }

        private bool CanUpdate(object commandParameter)
        {
            return (IsDirty && !HasErrors);
        }

        public async void OnCancel(object parameter)
        {
            var okToClose = true;
            if (IsDirty)
            {
                var options = new MetroDialogSettings { AffirmativeButtonText = "Yes", NegativeButtonText = "No" };
                var result = await ((MetroWindow)Owner).ShowMessageAsync("Vault settings changed!", "Are you sure you want to close and lose the changes you made?", MessageDialogStyle.AffirmativeAndNegative, options);
                if (result != MessageDialogResult.Affirmative)
                    okToClose = false;
            }

            if (okToClose)
            {
                SettingsManager.Instance.LoadSettings();
                TryClose();
            }
        }

        public void OnBrowse(object parameter)
        {
            var filter = (parameter.ToString() == "GPG") ? "GPG Executable|gpg.exe|" : "GIT Executable|git.exe|";
            var path = ShowDialog(filter);
            if (!string.IsNullOrEmpty(path))
            {
                if (parameter.ToString() == "GPG")
                    _settings.Programs.Gpg = path;
                else
                    _settings.Programs.Git = path;
            }
        }

        public void OnNewProfile(object parameter) => ShowProfileDialog();

        public void OnEditProfile(object parameter) => ShowProfileDialog(SelectedProfile);

        public bool CanEditProfile(object parameter) => SelectedProfile.IsNotNull() && SelectedItems.Count() == 1;

        public async void OnDeleteProfile(object parameter)
        {
            var okToDelete = true;
            var options = new MetroDialogSettings { AffirmativeButtonText = "Yes", NegativeButtonText = "No" };
            var result = await ((MetroWindow)Owner).ShowMessageAsync("Delete profiles!", "Are you sure you want to delete the selected profiles?", MessageDialogStyle.AffirmativeAndNegative, options);
            if (result != MessageDialogResult.Affirmative)
                okToDelete = false;

            if (okToDelete)
            {
                var profiles = SelectedItems.ToList();
                foreach (var profile in profiles)
                    Settings.Profiles.Remove(profile);

                SettingsManager.Instance.SaveSettings(Settings, false);
            }
        }

        public bool CanDeleteProfile(object parameter) => SelectedProfile.IsNotNull() && SelectedItems.Any();

        public void OnDefaultProfile(object parameter)
        {
            foreach (Profile profile in Settings.Profiles)
            {
                if (!EqualityComparer<Profile>.Default.Equals(profile, SelectedProfile))
                    profile.IsDefault = false;
            }
            SelectedProfile.IsDefault = true;
        }
        public bool CanDefaultProfile(object parameter) => SelectedProfile.IsNotNull() && SelectedItems.Count() == 1 && !SelectedProfile.IsDefault;

        private void OnSelectionChanged(object parameter) => RaiseCommandsCanExecute();

        private void OnPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) => IsDirty = true;

        private void OnCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e) => IsDirty = true;

        private void ShowProfileDialog(Profile selectedProfile = null)
        {
            var profileWindow = new Views.ProfileView(selectedProfile);
            profileWindow.Owner = Owner;
            profileWindow.ShowDialog();
        }

        private string ShowDialog(string filter)
        {
            var path = string.Empty;
            var dialog = new OpenFileDialog();
            dialog.Title = "Select Program";
            dialog.Filter = filter + "All Files (*.*)|*.*";

            if (dialog.ShowDialog() == true)
                path = dialog.FileName;
            return path;
        }
    }
}
