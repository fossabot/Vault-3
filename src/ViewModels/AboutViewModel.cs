using Seemon.Vault.Helpers;
using Seemon.Vault.Models;
using Seemon.Vault.Views;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Seemon.Vault.ViewModels
{
    public class AboutViewModel : ViewModelBase
    {
        private RelayCommand _navigateCommand;
        private RelayCommand _licenseCommand;
        private AsyncRelayCommand _checkUpdateCommand;
        private About _about;

        private UpdateManager _updateManager;
        private Settings _settings;
        private string _updateLabel;
        private bool _checkingUpdate = false;
        private bool? _updateFound;

        public AboutViewModel(Window owner)
            : base(owner)
        {
            _about = new About();
            _updateManager = UpdateManager.Instance;
            _settings = SettingsManager.Instance.Settings;
            Owner.Activated += OnOwnerActivated;
        }

        private void OnOwnerActivated(object sender, EventArgs e) => SetUpdateLabel();

        public string Title
        {
            get { return _about.Title; }
        }

        public string Version
        {
            get { return string.Format("V {0}", _about.Version); }
        }

        public string Author
        {
            get { return string.Format("Developed by {0}", _about.Author); }
        }

        public string Description
        {
            get { return _about.Description; }
        }

        public string Copyright
        {
            get { return _about.Copyright; }
        }

        public string UpdateInterval
        {
            get
            {
                var difference = DateTime.Now - _settings.Updates.LastChecked;

                if (difference.Days > 1)
                    return $"on {_settings.Updates.LastChecked.ToString("dd MMM")}, at {_settings.Updates.LastChecked.ToString("h:mm tt")}";
                else if (difference.Days > 0)
                    return $"yesterday, at {_settings.Updates.LastChecked.ToString("h:mm tt")}";
                else if (difference.Hours > 6)
                    return $"today, at {_settings.Updates.LastChecked.ToString("h:mm tt")}";
                else if (difference.Hours > 1)
                    return $"{difference.Hours} hours ago";
                else if (difference.Hours > 0)
                    return $"an hour ago";
                else if (difference.Minutes > 30)
                    return $"less than an hour ago";
                else if (difference.Minutes > 5)
                    return $"{difference.Minutes} minutes ago";
                else if (difference.Minutes > 1)
                    return $"a few minutes ago";
                else if (difference.Minutes > 0)
                    return $"a minute ago";
                else if (difference.Seconds > 10)
                    return $"less than a minute ago";
                else if (difference.Seconds > 5)
                    return $"a few seconds ago";
                return "just now";
            }
        }

        public string UpdateLabel
        {
            get { return _updateLabel; }
            set { SetProperty(ref _updateLabel, value); }
        }

        public bool CheckingUpdate
        {
            get { return _checkingUpdate; }
            set { SetProperty(ref _checkingUpdate, value); }
        }

        public ICommand NavigateCommand
        {
            get
            {
                if (_navigateCommand == null)
                    _navigateCommand = RegisterCommand(OnNavigate);
                return _navigateCommand;
            }
        }

        public ICommand LicenseCommand
        {
            get
            {
                if (_licenseCommand == null)
                    _licenseCommand = RegisterCommand(OnLicense);
                return _licenseCommand;
            }
        }

        public ICommand CheckUpdateCommand
        {
            get
            {
                if (_checkUpdateCommand == null)
                    _checkUpdateCommand = RegisterCommand(OnCheckUpdate);
                return _checkUpdateCommand;
            }
        }

        public void OnNavigate(object parameter)
        {
            if (parameter.IsNotNull())
                Process.Start(new ProcessStartInfo { FileName = parameter.ToString(), UseShellExecute = true });
        }

        public void OnLicense(object parameter)
        {
            var licenseWindow = new LicenseView();
            licenseWindow.Owner = Owner;
            licenseWindow.ShowDialog();
        }

        public async Task OnCheckUpdate()
        {
            CheckingUpdate = true;

            UpdateLabel = "Looking for updates....";

            _updateFound = await _updateManager.CheckForUpdates();
            SetUpdateLabel();

            if (_updateFound.HasValue && _updateFound.Value)
            {
                var updateWindow = new UpdateView();
                updateWindow.Owner = Owner;
                updateWindow.ShowDialog();
            }

            CheckingUpdate = false;
        }

        private void SetUpdateLabel()
        {
            if (!_updateFound.HasValue)
            {
                UpdateLabel = $"Update last checked {UpdateInterval}.";
            }
            else if (_updateFound.Value)
            {
                UpdateLabel = $"New update available (last checked {UpdateInterval})";
            }
            else
            {
                UpdateLabel = $"You have the latest version (last checked {UpdateInterval})";
            }
        }
    }
}
