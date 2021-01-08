using Seemon.Vault.Helpers;
using Seemon.Vault.Models;
using System;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace Seemon.Vault.ViewModels
{
    public class ProfileViewModel : ViewModelBase
    {
        private Profile _oldProfile = null;
        private Profile _profile = null;
        private RelayCommand _updateCommand;
        private RelayCommand _cancelCommand;
        private RelayCommand _browseCommand;

        public ProfileViewModel(Window owner, Profile profile = null)
            : base(owner)
        {
            if (profile != null)
            {
                _oldProfile = profile;
                _profile = profile.DeepCopy();
            }
            else
                _profile = new Profile { Name = string.Empty, Location = string.Empty, IsDefault = false };

            _profile.PropertyChanged += OnProfilePropertyChanged;
        }

        public Profile Profile
        {
            get { return _profile; }
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

        public void OnUpdate(object parameter)
        {
            if (_oldProfile.IsNotNull())
                SettingsManager.Instance.Settings.Profiles.Replace(_oldProfile, _profile);
            else
                SettingsManager.Instance.Settings.Profiles.Add(_profile);
            TryClose();
        }

        public bool CanUpdate(object commandParameter) => !Profile.HasErrors && !string.IsNullOrEmpty(Profile.Location) && !string.IsNullOrEmpty(Profile.Name);

        public void OnCancel(object parameter) => TryClose();

        public void OnBrowse(object parameter)
        {
            using var dialog = new FolderBrowserDialog
            {
                Description = "Select root path to vault.",
                UseDescriptionForTitle = true,
                SelectedPath = string.IsNullOrEmpty(Profile.Location) ? Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + Path.DirectorySeparatorChar : Profile.Location,
                ShowNewFolderButton = true
            };

            if (dialog.ShowDialog() == DialogResult.OK)
                Profile.Location = dialog.SelectedPath;
        }

        private void OnProfilePropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) => IsDirty = true;
    }
}
